using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ScholarshipManagement;
using ScholarshipManagement.Helper;
using ScholarshipManagement.Helper.Translations;
using ScholarshipManagement.Services.Common;
using ScholarshipManagement.Services.Ngo;
using ScholarshipManagement.Services.School;
using ScholarshipManagement.Services.SuperAdmin;
using ScholarshipManagement.Services.University;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


//builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<App>("body");
builder.RootComponents.Add<HeadOutlet>("head::after");

// MudBlazor
builder.Services.AddMudServices();


// jwt authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<CustomAuthStateProvider>()
);

builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    //client.BaseAddress = new Uri("https://localhost:7000/api/");
    client.BaseAddress = new Uri("https://sm-api.runasp.net/api/");
})
.AddHttpMessageHandler<AuthTokenHandler>();


//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri("https://sm-api.runasp.net/api/")
//    BaseAddress = new Uri("https://localhost:7000/api/")
//});



// Services
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<CommonService>();

builder.Services.AddScoped<GeneralSettingService>();
builder.Services.AddScoped<MasterDropdownService>();
builder.Services.AddScoped<MasterCountryService>();
builder.Services.AddScoped<MasterCurrencyService>();
builder.Services.AddScoped<LabelService>();
builder.Services.AddScoped<UsersMenuService>();
builder.Services.AddScoped<UsersRoleService>();
builder.Services.AddScoped<UsersRolePagesService>();
builder.Services.AddScoped<UsersLoginLogService>();
builder.Services.AddScoped<UsersLoginRoleService>();
builder.Services.AddScoped<UsersLoginService>();
builder.Services.AddScoped<HrStaffService>();

builder.Services.AddScoped<MasterSchoolService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<StudentRequirementService>();

builder.Services.AddScoped<UniversityService>();
builder.Services.AddScoped<CourseTypeService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CourseRequirementService>();
builder.Services.AddScoped<UniversityDocumentService>();

builder.Services.AddScoped<ApprovalService>();

// Permission Check
builder.Services.AddScoped<PermissionState>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddSingleton<CurrentUserProfileService>();


builder.Services.AddScoped<TranslationManager>();
builder.Services.AddScoped<TranslationStorageService>();
builder.Services.AddSingleton<TranslationStateService>();


await builder.Build().RunAsync();
