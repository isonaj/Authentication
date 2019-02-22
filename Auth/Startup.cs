using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Data;
using Auth.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];
            var contextMigrations = typeof(AuthDbContext).Assembly.GetName().Name;
            services.AddDbContext<AuthDbContext>(o =>
                o.UseSqlServer(connectionString,
                    optionsBuilder => optionsBuilder.MigrationsAssembly(contextMigrations)));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;

                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/login";
                    options.UserInteraction.LogoutUrl = "/logout";
                })
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            optionsBuilder => optionsBuilder.MigrationsAssembly(contextMigrations))
                )
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            optionsBuilder => optionsBuilder.MigrationsAssembly(contextMigrations));
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<User>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Migrate DB here?

            app.UseIdentity();
            app.UseIdentityServer();

            app.UseMvc();
        }
    }
}
