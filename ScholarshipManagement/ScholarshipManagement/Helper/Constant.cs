namespace ScholarshipManagement.Helper
{
    public static class Constant
    {
        public static class LocalStorageKeys
        {
            public const string Token = "AuthToken";
            public const string TokenExpiry = "TokenExpiry";

            public const string LoginId = "LoginId";
            public const string LoginName = "LoginName";

            public const string ModuleId = "ModuleId";
            public const string ModuleName = "ModuleName";
            public const string StaffType = "StaffType";

            public const string CurrentRoleId = "CurrentRoleId";
            public const string CurrentRoleName = "CurrentRoleName";

            public const string AvailableRoles = "AvailableRoles";

            public const string Translations = "app_translations_";
        }


        public static class DateFormats
        {
            public const string DisplayDateFormat = "dd-MM-yyyy";
            public const string ApiDateFormat = "yyyy-MM-dd";
        }


        public static class DateTimeFormats 
        {
            public const string DisplayDateTimeFormat = "dd-MM-yyyy HH:mm:ss";
            public const string ApiDateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
        }


        public static class StringFormats 
        {
            public const string DecimalTwoPlaces = "0.00";
            
        }


        public static class Routes
        {
            public const string Home = "/home";
            public const string Dashboard = "/dashboard";
            public const string Login = "/login";
            public const string MyProfile = "/my-profile";
            public const string AccessDenied = "/access-denied";
            public const string ForgotUserName = "/forgot-username";
            public const string ForgotPassword = "/forgot-password";
            public const string ResetPassword = "/reset-password";
            public const string LoginWithCode = "/login-with-code";
            public const string SchoolRegistration = "/school-registration";

            public const string MasterDropdown = "/dropdowns";
            public const string MasterDropdownValues = "/dropdowns/value/{Id:long}/{DropdownName}";


            public const string UserRoles = "/roles";
            public const string UserMenu = "/menus";
            public const string UserRolePages = "/role-pages";


            public const string Country = "/countries";
            public const string Currency = "/currencies";
            public const string CurrencyConversion = "/currency-conversion";
            public const string CurrencyConversionByCurrencyId = "/currency-conversion/{Id:long}";
            public const string GeneralSettings = "/general-settings";

            public const string Dropdowns = "/dropdowns";
            public const string Labels = "/labels";

            public const string Logs = "/login-logs";
            public const string UserLoginRole = "/user-login-role";

            public const string SchoolRegistartion = "/school-registration";
            public const string SchoolCreate = "/school";
            public const string SchoolUpdate = "/school-update/{Id:long}";
            public const string SchoolProfile = "/school/profile/{Id:long}";
            public const string SchoolList = "/school-lists";
            public const string SchoolListByCountryID = "/school-lists/{countryId:long}";

            public const string UniversityRegistartion = "/university-registration";
            public const string UniversityUpdate = "/university-update/{Id:long}";
            public const string UniversityList = "/university-lists";

            public const string UniversityDocument = "/university-document";
            public const string UniversityDocumentUpdate = "/university-document-update/{Id:long}";
            public const string UniversityDocumentList = "/university-document-lists";

            public const string CourseType = "/course-type";
            public const string CourseTypeUpdate = "/course-type-update/{Id:long}";
            public const string CourseTypeList = "/course-type-list";

            public const string Course = "/course";
            public const string CourseUpdate = "/course-update/{Id:long}";
            public const string CourseList = "/course-list";

            public const string CourseRequirement = "/course-req";
            public const string CourseRequirementUpdate = "/course-req-update/{Id:long}";
            public const string CourseRequirementList = "/course-req-list";


            public const string HrStaff = "/hr-staff";
            public const string HrStaffUpdate = "/hr-staff-update/{Id:long}";
            public const string HrStaffView = "/hr-staff-view/{Id:long}";
            public const string HrStaffList = "/hr-staff-list";

            public const string StudentAdd = "/student-add";
            public const string StudentUpdate = "/student-update/{Id:long}";
            public const string StudentView = "/student-view/{Id:long}";
            public const string StudentList = "/student-list";
            public const string StudentListBySchoolId = "/student-list/{schoolId:long}";

            public const string StudentRequiremntList = "/student-req-list";
            public const string Enrollments = "/enrollments";
            public const string EnrolledStudentsByReqId = "/enrolled/students/{reqId}";
            public const string EnrolledStudents = "/enrolled/students";
            public const string EnrolledStudentData = "/enrolled/student/data/{stdId:long}/{reqId:long}";


            public const string CountrySchoolSummary = "/country-schools-summary";

        }


        public static class ApiEndpoints
        {

            public const string AuthLogin = "auth/login";
            public const string AuthLogout = "auth/logout";
            public const string AuthSwitchRole = "auth/switch-role";
            public const string AuthForgotUserName = "auth/forgot-username";
            public const string AuthForgotPassword = "auth/forgot-password";
            public const string AuthResetPassword = "auth/reset-password";
            public const string AuthResetUsername = "auth/reset-username";
            public const string AuthLoginWithCode = "auth/login-with-code";
            public const string AuthVerifyLoginCode = "auth/verify-login-code";
            public const string AuthMyProfile = "auth/my-profile";
            public const string AuthUpdateMyProfile = "auth/update/my-profile";


            public const string GetAllUsersModule = "common/user-modules";
            public const string GetAllMenu = "common/load-menus";

        }

    }
}
