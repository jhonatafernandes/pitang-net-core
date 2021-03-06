using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pitang.Sms.NetCore.Auth;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Services;
using Pitang.Sms.NetCore.Services.Impl;
using Pitang.Sms.NetCore.Mapper.profiles;
using Pitang.Sms.NetCore.Mapper;
using AutoMapper;
using Pitang.Sms.NetCore.Repositories;
using Pitang.Sms.NetCore.Repositories.Impl;
using Pitang.Sms.NetCore.UnitOfWork;
using Pitang.Sms.NetCore.UnitOfWork.Impl;

namespace sms_pitang_netcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IStorieService, StorieService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IHistoricPasswordService, HistoricPasswordService>();
            services.AddScoped<ICriptographyService, CriptographyService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<MapperConfig>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false


                };
            });

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString"), b => b.MigrationsAssembly("sms-pitang-netcore")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
