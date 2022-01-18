using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RKC.Cursos.Aulas.Services;
using RKC.Cursos.Authentications;
using RKC.Cursos.Authentications.Services;
using RKC.Cursos.Context;
using RKC.Cursos.Modulos.Services;
using RKC.Cursos.Users.Services;

namespace RKC.Cursos
{
    public class Startup
    {
        public IConfiguration Configuration { get; }   
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationKey = Encoding.ASCII.GetBytes(Configuration.GetSection("CursosSettings").GetSection("AuthenticationKey").Value);

            services
                .AddSingleton(Configuration)
                .AddDbContext<CursosContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                })
                .AddTransient<IAuthenticationService, AuthenticationService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICredentialRepositoryService, CredentialRepositoryService>()
                .AddTransient<IModuloRepositoryService, ModuloRepositoryService>()
                .AddTransient<IAulaRepositoryService, AulaRepositoryService>()
                .AddCors()
                .AddControllers();
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(authenticationKey),
                            ValidateIssuer = true,
                            ValidIssuer = "RKC.Cursos",
                            ValidateAudience = false,
                            ValidateLifetime = false
                        };
                    });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(e => e
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}