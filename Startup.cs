using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProductsApi.Services.Products;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using ProductsApi.Services.Brands;

namespace ProductsApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var allowedCorsOrigins = Configuration.GetValue<string>("AllowedCorsOrigins");

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        // If deploying to Azure App Services, go to the App Service Configuration and add a AllowedCorsOrigins setting.
                        policy.WithOrigins(allowedCorsOrigins).AllowAnyHeader().AllowAnyMethod(); //.AllowAnyOrigin();
                        // https://learn.microsoft.com/en-us/answers/questions/844826/cors-error-while-fetching-data-from-aspnet-core-we.html
                    });
            });
            

            services.AddControllers().AddJsonOptions(ops =>
            {
                ops.JsonSerializerOptions.IgnoreNullValues = true;
                ops.JsonSerializerOptions.WriteIndented = true;
                ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddDbContextPool<ProductContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsApi", Version = "v1" });
            });
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBrandService, BrandService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
