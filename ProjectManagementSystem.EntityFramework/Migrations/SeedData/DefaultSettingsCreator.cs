using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using ProjectManagementSystem.EntityFramework;

namespace ProjectManagementSystem.Migrations.SeedData
{
    public class DefaultSettingsCreator
    {
        private readonly ProjectManagementSystemDbContext _context;

        public DefaultSettingsCreator(ProjectManagementSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "teumessian@qq.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "TeamLeader");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "smto.qq.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "teumessian@qq.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "ddnwddadgcznbdij");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Domain, "");
            AddSettingIfNotExists(EmailSettingNames.Smtp.EnableSsl, "true");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UseDefaultCredentials, "false");

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}