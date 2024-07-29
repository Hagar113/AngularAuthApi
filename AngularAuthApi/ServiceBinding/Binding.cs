using DataProvider.Provider;
using DataProvider.IProvider;
using DataAccess.IRepo;
using DataAccess.Repo;

namespace AngularAuthApi.ServiceBinding
{
    public static class Binding
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthProvider, AuthProvider>();

            services.AddScoped<IStudentProvider, StudentProvider>();

            services.AddScoped<ITeacherProvider, TeacherProvider>();
            services.AddScoped<IAdminProvider, AdminProvider>();

          


            


            return services;
        }
    }
}
