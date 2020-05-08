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
using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog
{
    public class Startup
    {
        public static Database database = new Database();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

      /*      cat.Add(new Category("New"));
            cat.Add(new Category("Dresses"));
            cat.Add(new Category("Skirts"));
            cat.Add(new Category("Blouse"));*/

      /*      pat.Add(new Pattern("Dresses A", 150, "Easy", new List<int>() { 0,1 }));
            pat.Add(new Pattern("Dresses B", 200, "Average", new List<int>() { 1 }));
            pat.Add(new Pattern("Dresses C", 200, "Advanced", new List<int>() { 1 }));
            pat.Add(new Pattern("Skirts A", 100, "Easy", new List<int>() { 0, 2 }));
            pat.Add(new Pattern("Skirts B", 50, "Average", new List<int>() { 2 }));
            pat.Add(new Pattern("Blouse A", 200, "Advanced", new List<int>() { 0, 3 }));*/
          
            /*Database d1 = new Database(pat,cat);
            Database d2 = new Database(pat, cat);
            Database d3 = new Database(pat, cat);
            Database d4 = new Database(pat, cat);
            database.GetPatterns();
           d1.GetPatternsFromCategory("New");
            d2.GetPatternsFromLevel("Easy");
            d3.ChangePrice(4,100);
            d4.DeleteCategory(5,"Blouse");*/
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
            opt.UseInMemoryDatabase("Database"));
            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
