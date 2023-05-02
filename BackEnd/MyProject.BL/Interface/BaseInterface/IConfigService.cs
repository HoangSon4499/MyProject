using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Interface
{
    public interface IConfigService
    {
        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConnectString(string key);

        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetAppSetting(string key, string valueDefault = null);
    }
}
