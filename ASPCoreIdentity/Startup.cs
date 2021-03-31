using ASPCoreIdentity.Models;
using ASPCoreIdentity.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPCoreIdentity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();

            var defaultconn = Configuration["ConnectionStrings:DefaultConnection"];
            var testapisecretkey  = Configuration["Movies:ServiceApiKey"];
            //https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows
            // var builder = new SqlConnectionStringBuilder(
            //Configuration.GetConnectionString("Movies"));
            // builder.Password = Configuration["DbPassword"];
            // var _connection = builder.ConnectionString;


            services.AddDbContextPool<CompanyDBContext>(opt =>
            {
                //opt.UseLazyLoadingProxies();
               // opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //var builder = services.AddIdentityCore<ApplicationUser>();
            //var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            //identityBuilder.AddEntityFrameworkStores<EmployeeDBContext>();
            //identityBuilder.AddSignInManager<SignInManager<ApplicationUser>>();


            //services.AddIdentity<IdentityUser, IdentityRole>(
            //    options => {
            //        options.Password.RequiredLength = 5;
            //        options.Password.RequiredUniqueChars = 1;
            //    })
            //   .AddEntityFrameworkStores<CompanyDBContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {

                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 1;
                    options.SignIn.RequireConfirmedEmail = true;  // for email confirmation

                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                })
                .AddEntityFrameworkStores<CompanyDBContext>()
                .AddDefaultTokenProviders().
                AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation");

            //sets token vlaidity to 5 hours
            // Changes token lifespan of all token types
            services.Configure<DataProtectionTokenProviderOptions>(o =>
                    o.TokenLifespan = TimeSpan.FromHours(5));

            // Changes token lifespan of just the Email Confirmation Token type
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>
                    o.TokenLifespan = TimeSpan.FromDays(3));


            // this is required if the views are not refreshing in .net 3.1
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddXmlSerializerFormatters();


            var sec = Configuration.GetSection("Tokens").GetSection("GoogleClientId").Value;

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Configuration.GetSection("Tokens").GetSection("GoogleClientId").Value; 
                 options.ClientSecret = Configuration.GetSection("Tokens").GetSection("GoogleClientSecret").Value;
             }).AddFacebook(options =>
            {
                options.AppId = Configuration.GetSection("Tokens").GetSection("FaceBookAppId").Value;
                options.AppSecret = Configuration.GetSection("Tokens").GetSection("FaceBookClientSecret").Value;
            });


            services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy", policy =>
                            policy.RequireClaim("Delete Role"));
                options.AddPolicy("AdminRolePolicy", policy =>
                                  policy.RequireRole("Admin"));
                //options.AddPolicy("EditRoleClaimPolicy", policy =>
                //                  policy.RequireClaim("Edit Role", "true"));
               // Member of the Admin role AND have Edit Role claim with a value of true
                //OR
                //Member of the Super Admin role
                //options.AddPolicy("EditRoleClaimPolicy", policy => policy.RequireAssertion(context =>
                //               context.User.IsInRole("Admin") &&
                //               context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                //               context.User.IsInRole("SuperAdmin")
                //            ));

                // Adding custom authorization

                options.AddPolicy("EditRoleClaimPolicy", policy =>
                        policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));



                options.AddPolicy("DeleteRoleClaimPolicy", policy =>
                                  policy.RequireClaim("Delete Role"));
                // multiple roles in the policy
                options.AddPolicy("SuperAdminPolicy", policy =>
                                  policy.RequireRole("Admin", "User", "Manager"));

                options.AddPolicy("CityPolicy", policy =>
                                  policy.AddRequirements(new CityPolicyRequirment()) );


                //options.AddPolicy("AllowedCountryPolicy",
                //        policy => policy.RequireClaim("Country", "USA", "India", "UK"));

                //if you do not want the rest of the handlers to be called, when a failure is returned
                // options.InvokeHandlersAfterFailure = false; 
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register the first handler
            services.AddSingleton<IAuthorizationHandler,CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            // Register the second handler
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();


            services.AddTransient<IAuthorizationHandler, CityPolicyRequirmentHandler>();




  
            services.AddSingleton<DataProtectionPurposeStrings>();

            //services.Configure<IdentityOptions>(option =>
            //{
            //    option.Password.RequiredLength = 5;
            //    option.Password.RequiredUniqueChars = 1;

            //});

            services.AddMvc(opt => { opt.EnableEndpointRouting = false; });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
            //});




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                       //app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseStaticFiles();
            //app.UseMvc();

            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();

            app.UseRouting();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  "default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            //});


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
