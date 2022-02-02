using AJ3.Core.Contracts;
using AJ3.Core.Data.DataManager;
using AJ3.Core.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AJ3.WebApp.Infrastructure.Installers
{
    public class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Student
            services.AddTransient<IStudentManager, StudentManager>();
            services.AddTransient<IStudentCourseManager, StudentCourseManager>();
            //Order
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IOrderDetailManager, OrderDetailManager>();
            services.AddTransient<IOrderAdjustmentManager, OrderAdjustmentManager>();

            //course
            services.AddTransient<ICourseCategoryManager, CourseCategoryManager>();
            services.AddTransient<ICourseManager, CourseManager>();

            //payment 
            services.AddTransient<IOrderPaymentManager, OrderPaymentManager>();

            //report
            services.AddTransient<IReportManager, ReportManager>();

            services.AddTransient<IDbManager, DbManager>();

            //other
            services.AddTransient<ILicenseNoteManager, LicenseNoteManager>();
            services.AddTransient<IStudentLicenseNoteManager, StudentLicenseNoteManger>();

            //setting
            services.AddTransient<ICompanyProfileManager, CompanyProfileManager>();
        }
    }
}
