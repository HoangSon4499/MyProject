using MyProject.BL.BL;
using MyProject.BL.Interface;
using MyProject.BL.Interface.BaseInterface;

namespace MyProject.ConfigDependencyInjection
{
    public static class BaseServiceInstaller 
    {
        /// <summary>
        /// Hàm xử lý dependecy inject base
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection BaseServiceInstallerApp(this IServiceCollection services, IConfiguration configuration)
        {
            DBServiceInstaller(services);
            BaseServiceCollectionInstaller(services);
            return services;
        }

        /// <summary>
        /// Hàm xử lý DI service database
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void DBServiceInstaller(this IServiceCollection services)
        {
            services.AddTransient<IDataBaseService, DataBaseServiceBL>();
        }

        public static void BaseServiceCollectionInstaller(this IServiceCollection services)
        {
            services.AddTransient<BaseServiceCollection, BaseServiceCollection>();
            services.AddTransient<IConfigService, ConfigServiceBL>();
            services.AddTransient<IBaseBL, BaseBL>();
        }
    }
}
