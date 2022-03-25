using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions // service extension classlarımız static olacak
    // böylece kullanmak için init etmemize gerek yok
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config){
            services.AddScoped<ITokenService, TokenService>();
            // lifetime of http request equals to addscoped
            services.AddDbContext<DataContext>(options =>{
                // sth like lambda method
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}