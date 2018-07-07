using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Net.Mail.Smtp;
using Abp.Notifications;
using Abp.Runtime.Session;
using AutoMapper;
using FormCheck;
using ProjectManagementSystem.Authorization;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.Projects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects
{
    public class ProjectAppService : ProjectManagementSystemAppServiceBase, IProjectAppService
    {

        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly ISmtpEmailSenderConfiguration _smtpEmialSenderConfig;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IAbpSession _abpSession;

        [DllImport(@"../../../TimeString.dll", EntryPoint = "convert_t2s", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        extern static string convert_t2s(DateTime dateTime);

        [DllImport(@"../../../TimeString.dll", EntryPoint = "convert_s2t", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        extern static DateTime convert_s2t(string dateTime);

        public ProjectAppService(IRepository<Project> projectRepository, IRepository<User, long> userRepository,
            ISmtpEmailSenderConfiguration smtpEmialSenderConfigtion, INotificationPublisher notificationPublisher,
            IAbpSession abpSession)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _smtpEmialSenderConfig = smtpEmialSenderConfigtion;
            _notificationPublisher = notificationPublisher;

            _abpSession = abpSession;
        }

        public ProjectSearchOutputDto SearchProjects(ProjectSearchInputDto input)
        {
            var query = _projectRepository.GetAll();

            var currentTenant = _abpSession.TenantId;
            if (currentTenant.HasValue)
            {
                query = query.Where(t => t.TenantId == currentTenant);
            }

            if (input.TeamLeaderId.HasValue)
            {
                query = query.Where(t => t.TeamLeaderId == input.TeamLeaderId);
            }

            if (input.State.HasValue)
            {
                query = query.Where(t => t.State == input.State);
            }

            var list = query.ToList();
            if (currentTenant != null)
            {
                foreach (var project in list)
                {
                    if (project.TeamLeaderId.HasValue)
                        project.TeamLeader = _userRepository.Get(ObjectMapper.Map<long>(project.TeamLeaderId));
                }
            }

            return new ProjectSearchOutputDto
            {
                Projects = Mapper.Map<List<ProjectDto>>(list)
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Projects)]
        public int CreateProject(CreateProjectDto input)
        {
            Logger.Info("Creating a project for input: " + input);

            var project = new Project
            {
                TenantId = _abpSession.TenantId,

                Name = input.Name,
                Description = input.Description,
                StartTime = input.StartTime,
                DeliverTime = input.DeliverTime,
                TeamLeaderId = input.TeamLeaderId
            };

            if (input.TeamLeaderId.HasValue)
            {
                var user = _userRepository.Get(ObjectMapper.Map<long>(input.TeamLeaderId));
                project.TeamLeader = user;

                string message = "A new project -- \"" + input.Name + "\" has being assigned to u.";
                _notificationPublisher.Publish("New Project", new MessageNotificationData(message), null, NotificationSeverity.Info, new[] { user.ToUserIdentifier() });
            }

            return _projectRepository.InsertAndGetId(project);
        }

        [AbpAuthorize(PermissionNames.Pages_Projects)]
        public void DeleteProject(int projectId)
        {
            var project = _projectRepository.Get(projectId);
            if (project != null)
            {
                _projectRepository.Delete(project);
            }
        }

        //not realized
        public IList<ProjectDto> GetAllProjects()
        {
            var query = _projectRepository.GetAll();

            throw new NotImplementedException();
        }

        public ProjectDto GetProjectById(int projectId)
        {
            var project = _projectRepository.Get(projectId);

            return project.MapTo<ProjectDto>();
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);

            return project.MapTo<ProjectDto>();
        }

        [AbpAuthorize(PermissionNames.Pages_Projects_EditState)]
        public void UpdateProject(UpdateProjectDto input)
        {
            Logger.Info("Updating a project for input: " + input);

            var project = _projectRepository.Get(input.Id);

            project.State = input.State;

            if (input.Name != project.Name || input.Description != project.Description || input.StartTime != project.StartTime
                || input.DeliverTime != project.DeliverTime || input.TeamLeaderId != project.TeamLeaderId)
            {
                PermissionChecker.Authorize(PermissionNames.Pages_Projects_EditOthers);
            }

            project.Name = input.Name;
            project.Description = input.Description;
            project.StartTime = input.StartTime;
            project.DeliverTime = input.DeliverTime;
            
            if (input.TeamLeaderId.HasValue)
            {
                var user = _userRepository.Get(ObjectMapper.Map<long>(input.TeamLeaderId));

                if (input.TeamLeaderId != project.TeamLeaderId)
                {
                    string message = "A new project -- \"" + input.Name + "\" has being assigned to u.";
                    _notificationPublisher.Publish("New Project", new MessageNotificationData(message), null, NotificationSeverity.Info, new[] { user.ToUserIdentifier() });
                }

                project.TeamLeaderId = input.TeamLeaderId;
                project.TeamLeader = user;
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Projects)]
        public void SendEmail(int teamLeaderId, string name)
        {
            if(_abpSession.TenantId == null)
                return;
            var user = _userRepository.Get(ObjectMapper.Map<long>(teamLeaderId));

            SmtpEmailSender emailSender = new SmtpEmailSender(_smtpEmialSenderConfig);
            string message = "Be aware of you task project -- " + name + ", which is approaching its deliver time.";
            emailSender.Send("teumessian@qq.com", user.EmailAddress, "New Todo item", message);
        }

        public ProjectDto CheckForm(CreateProjectDto dto)
        {
            var form = new Form
            {
                name = dto.Name,
                description = dto.Description
            };

            var formChecker = new FormChecker(form, 32, 64, 32);
            if (formChecker.Check())
            {
                return ObjectMapper.Map<ProjectDto>(dto);
            }
            else
            {
                var newForm = formChecker.Modify(form);
                return new ProjectDto
                {
                    Name = newForm.name,
                    Description = newForm.description,
                };
            }

        }
    }
}
