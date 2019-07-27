using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workrep.Backend.DatabaseIntegration.Models;
using System.IdentityModel.Tokens.Jwt;
using Workrep.Backend.API.Services;
using Microsoft.Extensions.Options;
using Workrep.Backend.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;

namespace Workrep.Backend.API
{
    public class Startup
    {

        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {   
            // Workrep Databasecontext
            services.AddDbContext<WorkrepContext>(options => options.UseSqlServer(Configuration.GetSection("Connectionstrings").GetValue<string>("Workrep")));

            //Authentication Configuration
            var authService = new AuthenticationService();
            Configuration.GetSection("Authentication").Bind(authService);
            services.AddSingleton(authService);

            //Register MVC with Fluentvalidation
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Validators.UserRegistrationValidator>());

            //Defining the Swagger genearator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info() { Title = "Workrep Core API", Version = "1.0.0" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddFluentValidationRules();
            });

            //Email salt
            services.AddScoped<EmailSaltService>(provider =>
            {
                var emailSaltService = new EmailSaltService();
                Configuration.GetSection("EmailSalt").Bind(emailSaltService);
                return emailSaltService;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workrep API V1");
            });

            app.UseAuthentication();
            app.UseMvc();
            
        }
    }
}
