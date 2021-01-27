using bookstore.Models;
using bookstore.Models.repositories;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreMVC.Models.repositories;

namespace bookstore
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
            
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddScoped<  IbookStoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<  IbookStoreRepository<Book>, BookDbRepository>();
            services.AddDbContext<BookStoreDbContext>(options =>
           {
               options.UseSqlServer(configuration.GetConnectionString("SqlCon"));


           });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(route => {
                route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}");
            });
            app.UseStaticFiles();
           
            
        }
    }
}
