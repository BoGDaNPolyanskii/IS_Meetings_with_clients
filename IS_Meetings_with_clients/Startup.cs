//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.OpenApi.Models;
//using System;
//using Microsoft.AspNetCore.Hosting;

//namespace IS_Meetings_with_clients
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddControllersWithViews();

//            // Register the Swagger generator, defining 1 or more Swagger documents
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo
//                {
//                    Version = "v1",
//                    Title = "Meetings API",
//                    Description = "API Documentation for IS_Meetings_with_clients project",
//                    TermsOfService = new Uri("https://example.com/terms"),
//                    Contact = new OpenApiContact
//                    {
//                        Name = "Your Name",
//                        Email = string.Empty,
//                        Url = new Uri("https://twitter.com/yourtwitter"),
//                    },
//                    License = new OpenApiLicense
//                    {
//                        Name = "Use under LICX",
//                        Url = new Uri("https://example.com/license"),
//                    }
//                });
//            });
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                // Enable middleware to serve generated Swagger as a JSON endpoint.
//                app.UseSwagger();
//                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
//                // specifying the Swagger JSON endpoint.
//                app.UseSwaggerUI(c =>
//                {
//                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetings API V1");
//                });
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
//            });
//        }
//    }
//}