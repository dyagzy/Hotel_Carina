using Hotel_Carina.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina
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

           

            /*this Ioc helps us to configure Cors so that any person not on 
            our netweork can be allow to access the Api.
            Basically CORS is used to either restrict or grant access to 
            people or network to have access to our Api 
            NEXT is that we need to go to the configure section of the Start up class and let
            the App knows that it should use the already configured Cors*/

            services.AddCors(ac => {
                ac.AddPolicy("AllowAllCorsPolicy", x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddDbContext<DataBaseContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel_Carina", Version = "v1" , Description = "This is an hotel managemnt " +
                    "Web Api, that" +
                    "allows customers to check in and out of their hotels, " +
                    "order for rooms, and get general details of the hotel"});
            });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel_Carina v1"));

            app.UseHttpsRedirection();

            //Tells the start up class to use the above configured Cors in the Ioc
            app.UseCors("AllowAllCorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
