using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Catalog
{
    public class Startup
        
    {
        public static List<Level> lev = new List<Level>();
        public static List<Category> cat = new List<Category>();
        public static List<Pattern> pat = new List<Pattern>();
        public static Database database = new Database(pat, cat, lev);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            lev.Add(new Level("Easy"));
            lev.Add(new Level("Average"));
            lev.Add(new Level("Advanced"));

            cat.Add(new Category("Temp"));
            cat.Add(new Category("New"));
            cat.Add(new Category("Dresses"));
            cat.Add(new Category("Skirts"));
            cat.Add(new Category("Blouse"));

            pat.Add(new Pattern("Dresses A", 150, 0, 2));
            pat.Add(new Pattern("Dresses B", 200, 0, 1));
            pat.Add(new Pattern("Dresses C", 200, 2, 2));
            pat.Add(new Pattern("Skirts A", 100, 1, 1));
            pat.Add(new Pattern("Skirts B", 50, 0, 3));
            pat.Add(new Pattern("Blouse A", 200, 2, 4));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddDbContext<TodoContext>(opt =>
                opt.UseInMemoryDatabase("TodoList"));
             services.AddControllers();

       /*     services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllersWithViews();*/
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
