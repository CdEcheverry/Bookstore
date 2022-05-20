using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Aplication.Authors;
using Aplication.Services;
using FluentValidation.AspNetCore;
using WebAPI.Middleware;
using System.Reflection;
using System;
using System.IO;

namespace WebAPI
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
            services.AddAutoMapper(typeof(GetAllAuthors.Handler));
            services.AddDbContext<BookStoreContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaulConnection"));
            });
            services.AddMediatR(typeof(GetAllAuthors.Handler).Assembly);
            services.AddControllers().AddFluentValidation(custom => custom.RegisterValidatorsFromAssemblyContaining<CreateAuthor>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                c.CustomSchemaIds(c => c.FullName);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            AddCustomServices(services);
        }

        private void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IValidationsService, ValidationsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HandlerErrorMiddleware>();
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

          //  app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
