using Microsoft.Extensions.Configuration;
using MyProject.BL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.BL
{
    public class ConfigServiceBL : IConfigService
    {
        private readonly IConfiguration _configuration;
        private IConfigurationSection _appSettings;
        private IConfigurationSection _connectionString;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigServiceBL(IConfiguration configuration)
        {
            _configuration = configuration;
            ConfigService();
        }

        /// <summary>
        /// Hàm xử lý set giá trị cho ConfigurationSection
        /// </summary>
        public void ConfigService()
        {
            _appSettings = _configuration.GetSection("AppSettings");
            _connectionString = _configuration.GetSection("ConnectionString");
        }

        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConnectString(string key)
        {
            var connectionString = _connectionString[key];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return null;
            }
            else
            {
                return connectionString;
            }
        }

        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAppSetting(string key, string valueDefault = null)
        {
            var appSetting = _appSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting))
            {
                return valueDefault;
            }
            else
            {
                return appSetting;
            }
        }
    }
}
