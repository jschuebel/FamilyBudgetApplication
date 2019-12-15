//using AutoMapper;
//using MediatR;
using Microsoft.Extensions.DependencyInjection;
//using System.Reflection;

namespace FamilyBudget.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped<Interface.ICategoryXrefService, Services.CategoryXrefService>();
            services.AddScoped<Interface.ICategoryService, Services.CategoryService>();
            services.AddScoped<Interface.IProductService, Services.ProductService>();
            services.AddScoped<Interface.IPurchaseService, Services.PurchaseService>();
 
            return services;
        }
    }
}
