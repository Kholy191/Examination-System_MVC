using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.UnitofWork;
using Services;
using Services.CoreServices;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;

namespace WebExaminationApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
               optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MVC_ExaminationSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            #region Services

            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IAnswerOptionService, AnswerOptionService>();
            builder.Services.AddScoped<IQuestionServices, QuestionServices>();
            builder.Services.AddScoped<ICourseServices, CourseServices>();
            builder.Services.AddScoped<IInstructorServices, InstructorServices>();
            builder.Services.AddScoped<IStudentServices, StudentServices>();

            #endregion


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Authentication/SignIn";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Data Seeding Configuration
            using (var Scope = app.Services.CreateScope())
            {
                var dataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await dataSeeding.SeedIdentityDataAsync();
            }
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authentication}/{action=SignIn}/{id?}");

            app.Run();
        }
    }
}
