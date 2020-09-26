using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(
                options => options.UseNpgsql(connection));

            services.AddCors();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            	.AddJwtBearer(opt =>
            	{
            		opt.RequireHttpsMetadata = false;
            		opt.TokenValidationParameters = new TokenValidationParameters
            		{
            			ValidateIssuer	= true,
            			ValidIssuer = AuthOptions.ISSUER,
            			ValidateAudience = true,
            			ValidAudience = AuthOptions.AUDIENCE,
            			ValidateLifetime = true,
            			IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            			ValidateIssuerSigningKey = true
            		};
            	});
            
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(opt =>
            {
                opt.WithOrigins("https://localhost:5001/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                	name: "default",
                	pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}