using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyProject.BL.Interface;

namespace MyProject.BL.BL
{
    public class BaseServiceCollection
    {
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="serviceProvider"></param>
        public BaseServiceCollection(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// Lấy service theo type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetServiceByType<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        #region ===== _configService =====
        private IConfigService _configService;
        /// <summary>
        /// Hàm lấy service config
        /// </summary>
        /// <returns></returns>
        public IConfigService GetConfigService()
        {
            if(_configService == null)
            {
                return GetServiceByType<IConfigService>();
            }

            return _configService;
        }
        #endregion

        #region ===== DBService =====
        private IDataBaseService _dataBaseService;
        public IDataBaseService GetDataBaseService()
        {
            if (_dataBaseService == null)
            {
                return GetServiceByType<IDataBaseService>();
            }
            return _dataBaseService;
        }
        #endregion

    }
}
