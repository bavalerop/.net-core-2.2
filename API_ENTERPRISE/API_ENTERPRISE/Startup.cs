using API_ENTERPRISE.Data;
using API_ENTERPRISE.Extensions;
using API_ENTERPRISE.Repository;
using API_ENTERPRISE.Repository.Interfaces;
using API_ENTERPRISE.Services;
using API_ENTERPRISE.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API_ENTERPRISE
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
            //Inyeccion de repositorys
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            //Inyeccion de services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IConfiguracionService, ConfiguracionService>();
            services.AddScoped<IAuthService, AuthService>();
            
            //Agregar conexion al contexto principal
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Dbcnn")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Token
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

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

            services.AddMvc();

            #region Registro the Swagger generator, se define 1 o mas documentos
            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1, new Info {
                                        Title = SwaggerConfiguration.DocInfoTitle,
                                        Version = SwaggerConfiguration.DocInfoVersion,
                                        Description = SwaggerConfiguration.DocInfoDescription,
                                        Contact = contact
                                   });
                //Configuracion de swagger para solicitar el token
                swagger.AddSecurityDefinition("Bearer", new ApiKeyScheme {
                                                In = "header de la peticion",
                                                Description = "Ingrese en el campo la palabra 'Bearer' seguido de espacio y JWT Token",
                                                Name = "Authorization",
                                                Type = "apiKey"
                                             });
                swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {{ "Bearer", Enumerable.Empty<string>() },});
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Se habilita el middleware para usar el swagger-ui

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
                c.RoutePrefix = string.Empty;
            });

            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //Uso de JWT
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
