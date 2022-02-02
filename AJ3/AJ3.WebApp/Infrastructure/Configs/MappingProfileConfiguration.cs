using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using AJ3.WebApp.Models.Course;
using AJ3.WebApp.Models.Order;
using AJ3.WebApp.Models.Report;
using AJ3.WebApp.Models.Setting;
using AJ3.WebApp.Models.Student;
using AutoMapper;

namespace AJ3.WebApp.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<AvailableCourse, AvailableCourseViewModel>();
            CreateMap<CourseCategoryDetail, CourseCategorySelectViewModel>();
            CreateMap<NewStudent, StudentApplicationFormViewModel>().ReverseMap();

            CreateMap<Student, StudentDetailViewModel>();
            CreateMap<StudentCourseDetail, StudentCourseDetailViewModel>();
            CreateMap<StudentMasterList, StudentMasterListViewModel>();
            CreateMap<StudentCourse, StudentCourseDetailViewModel>();
            CreateMap<StudentDetailViewModel, StudentRequest>();

            CreateMap<CancelStudentCourseFormViewModel, CancelStudentCourseRequest>();
            CreateMap<ChangeStudentCourseFromViewModel, ChangeStudentCourseRequest>();
            //order
            CreateMap<OrderPaymentRequest, OrderPaymentFormViewModel>().ReverseMap();
            CreateMap<Order, StudentOrderViewModel>();
            CreateMap<OrderPayment, StudentOrderPaymentViewModel>();
            CreateMap<OrderPayment, OrderPaymentFormViewModel>();

            CreateMap<OrderAdjustment, OrderAdjustmentViewModel>();

            CreateMap<OrderMasterList, OrderMasterListViewModel>();
            CreateMap<OrderDetailList, OrderDetailListViewModel>();
            //report
            CreateMap<OrderPaymentReportList, OrderPaymentReportListViewModel>();
            //course
            CreateMap<CourseMasterList, CourseMasterListViewModel>();
            CreateMap<CourseMasterListViewModel,CourseRequest>();

            //other 
            CreateMap<LicenseNote, LicenseNoteViewModel>();

            CreateMap<CompanyProfileViewModel, CompanyProfileRequest>();
            CreateMap<CompanyProfile, CompanyProfileViewModel>();

        }
    }
}
