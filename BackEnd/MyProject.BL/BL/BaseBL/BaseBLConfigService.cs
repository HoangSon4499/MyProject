using MyProject.BL.Interface.BaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.BL
{
    public partial class BaseBL : IBaseBL
    {
        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConnectString(string key)
        {
            return _configService.GetConnectString(key);
        }

        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAppSetting(string key, string valueDefault = null)
        {
            return _configService.GetAppSetting(key, valueDefault);
        }
    }
}
