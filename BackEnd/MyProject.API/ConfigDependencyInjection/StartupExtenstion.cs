using MyProject.BL.BL;
using MyProject.BL.Interface;

namespace MyProject.ConfigDependencyInjection
{
    public static class StartupExtenstion
    {
        public static IServiceCollection ConfigStartupExtenstion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmployee, EmployeeBL>();
            return services;
        }
    }
}
