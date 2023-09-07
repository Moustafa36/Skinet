using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServicesExtentions 
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services , IConfiguration config )
        {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlite(connectionString:config.GetConnectionString("DefaultConnection"));
});

        services.AddScoped<IProductRepository, PorductRepository>();
        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<ApiBehaviorOptions>(options=> 
{
    options.InvalidModelStateResponseFactory= actioncontext =>
    {
        var error =actioncontext.ModelState 
        .Where(e=>e.Value.Errors.Count > 0)
        .SelectMany(x=>x.Value.Errors)
        .Select(X=>X.ErrorMessage).ToArray();

        var ErrorResponse = new ValidationErrorResponse{Errors = error }  ;
         return new BadRequestObjectResult(ErrorResponse);
    };
   
}
);

           return services;   
        }
        
    }

   
}