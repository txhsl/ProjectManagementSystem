using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Net.Mail.Smtp;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Threading;
using AutoMapper;
using FormCheck;
using ProjectManagementSystem.Authorization;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.Modules;
using ProjectManagementSystem.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects
{
    public class ModuleAppService : ProjectManagementSystemAppServiceBase, IModuleAppService
    {

        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly ISmtpEmailSenderConfiguration _smtpEmialSenderConfig;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IAbpSession _abpSession;

        [DllImport(@"../../../TimeString.dll", EntryPoint = "convert_t2s", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        extern static string convert_t2s(DateTime dateTime);

        [DllImport(@"../../../TimeString.dll", EntryPoint = "convert_s2t", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        extern static DateTime convert_s2t(string dateTime);

        public ModuleAppService(IRepository<Module> moduleRepository, IRepository<Project> projectRepository,
            IRepository<User, long> userRepository, ISmtpEmailSenderConfiguration smtpEmialSenderConfigtion,
            INotificationPublisher notificationPublisher, IAbpSession abpSession)
        {
            _moduleRepository = moduleRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _smtpEmialSenderConfig = smtpEmialSenderConfigtion;
            _notificationPublisher = notificationPublisher;

            _abpSession = abpSession;
        }

        public ModuleSearchOutputDto SearchModules(ModuleSearchInputDto input)
        {
            var query = _moduleRepository.GetAll();

            var currentTenant = _abpSession.TenantId;
            if (currentTenant.HasValue)
            {
                query = query.Where(t => t.TenantId == currentTenant);
            }

            if (input.MemberId.HasValue)
            {
                query = query.Where(t => t.MemberId == input.MemberId);
            }

            if (input.ProjectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == input.ProjectId);
            }

            if (input.State.HasValue)
            {
                query = query.Where(t => t.State == input.State);
            }

            var list = query.ToList();
            if (currentTenant != null)
            {
                foreach (var module in list)
                {
                    if (module.MemberId.HasValue)
                        module.Member = _userRepository.Get(ObjectMapper.Map<long>(module.MemberId));

                    if (module.ProjectId.HasValue)
                        module.Project = _projectRepository.Get(ObjectMapper.Map<int>(module.ProjectId));
                }
            }

            return new ModuleSearchOutputDto
            {
                Modules = Mapper.Map<List<ModuleDto>>(list)
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Modules)]
        public int CreateModule(CreateModuleDto input)
        {
            Logger.Info("Creating a module for input: " + input);

            var module = new Module
            {
                TenantId = _abpSession.TenantId,

                Name = input.Name,
                Description = input.Description,
                StartTime = input.StartTime,
                DeliverTime = input.DeliverTime,
                TechStack = input.TechStack,
                Level = input.Level,
                MemberId = input.MemberId,
                ProjectId = input.ProjectId
            };

            if (input.MemberId.HasValue)
            {
                var user = _userRepository.Get(ObjectMapper.Map<long>(input.MemberId));
                module.Member = user;

                string message = "A new module -- \"" + input.Name + "\" has being assigned to u.";
                _notificationPublisher.Publish("New Module", new MessageNotificationData(message), null, NotificationSeverity.Info, new[] { user.ToUserIdentifier() });
            }

            if (input.ProjectId.HasValue)
            {
                var project = _projectRepository.Get(ObjectMapper.Map<int>(input.ProjectId));
                module.Project = project;
            }

            return _moduleRepository.InsertAndGetId(module);
        }

        [AbpAuthorize(PermissionNames.Pages_Modules)]
        public void DeleteModule(int moduleId)
        {
            var module = _moduleRepository.Get(moduleId);
            if (module != null)
            {
                _moduleRepository.Delete(module);
            }
        }

        //not realized
        public IList<ModuleDto> GetAllModules()
        {
            var query = _moduleRepository.GetAll();

            throw new NotImplementedException();
        }

        public ModuleDto GetModuleById(int moduleId)
        {
            var module = _moduleRepository.Get(moduleId);

            return module.MapTo<ModuleDto>();
        }

        public async Task<ModuleDto> GetModuleByIdAsync(int moduleId)
        {
            var module = await _moduleRepository.GetAsync(moduleId);

            return module.MapTo<ModuleDto>();
        }

        [AbpAuthorize(PermissionNames.Pages_Modules_EditState)]
        public void UpdateModule(UpdateModuleDto input)
        {
            Logger.Info("Updating a module for input: " + input);

            var module = _moduleRepository.Get(input.Id);
            var currentUser = AsyncHelper.RunSync(this.GetCurrentUserAsync);

            module.State = input.State;

            //Check Permission for TeamLeader
            if (input.Name != module.Name || input.Description != module.Description || input.Level != module.Level || input.StartTime != module.StartTime 
                || input.DeliverTime != module.DeliverTime || input.TechStack != module.TechStack || input.MemberId != module.MemberId
                || input.ProjectId != module.ProjectId)
            {
                PermissionChecker.Authorize(PermissionNames.Pages_Modules_EditOthers);
            }

            module.Name = input.Name;
            module.Description = input.Description;
            module.Level = input.Level;
            module.StartTime = input.StartTime;
            module.DeliverTime = input.DeliverTime;
            module.TechStack = input.TechStack;

            if (input.MemberId.HasValue)
            {
                var user = _userRepository.Get(ObjectMapper.Map<long>(input.MemberId));

                if (input.MemberId != module.MemberId)
                {
                    string message = "A new module -- \"" + input.Name + "\" has being assigned to u.";
                    _notificationPublisher.Publish("New Module", new MessageNotificationData(message), null, NotificationSeverity.Info, new[] { user.ToUserIdentifier() });
                }

                module.MemberId = input.MemberId;
                module.Member = user;
            }

            if (input.ProjectId.HasValue)
            {
                module.ProjectId = input.ProjectId;
                var project = _projectRepository.Get(ObjectMapper.Map<int>(input.ProjectId));
                module.Project = project;
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Modules)]
        public void SendEmail(int memberId, string name)
        {
            if (_abpSession.TenantId == null)
                return;
            var user = _userRepository.Get(ObjectMapper.Map<long>(memberId));

            SmtpEmailSender emailSender = new SmtpEmailSender(_smtpEmialSenderConfig);
            string message = "Be aware of you task module -- " + name + ", which is approaching its deliver time.";
            emailSender.Send("teumessian@qq.com", user.EmailAddress, "New Todo item", message);
        }

        public ModuleDto CheckForm(CreateModuleDto dto)
        {
            var form = new Form
            {
                name = dto.Name,
                description = dto.Description,
                techstack = dto.TechStack
            };

            var formChecker = new FormChecker(form, 32, 64, 32);
            if (formChecker.Check())
            {
                return ObjectMapper.Map<ModuleDto>(dto);
            }
            else
            {
                var newForm = formChecker.Modify(form);
                return new ModuleDto
                {
                    Name = newForm.name,
                    Description = newForm.description,
                    TechStack = newForm.techstack
                };
            }

        }
    }
}
