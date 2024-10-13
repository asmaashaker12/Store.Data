using Microsoft.AspNetCore.Mvc;
using Store.Repository;
using Store.Repository.Basket;
using Store.Repository.Interfaces;
using Store.Service.CashServices;
using Store.Service.HandleResponse;
using Store.Service.OrderServices;
using Store.Service.OrderServices.OrderDtos;
using Store.Service.Services;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.Dtos;
using Store.Service.TokenService;
using Store.Service.UserServices;

namespace Store.Web.ApplicationServiceExtensions
{
    public static class ApplicationServiceExtensionscs
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICashServices, CashService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(BaskerProfile));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(OrderProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actioncontext =>
                {
                    var errors = actioncontext.ModelState
                    .Where(model => model.Value?.Errors.Count > 0)
                    .SelectMany(model => model.Value?.Errors).Select(x => x.ErrorMessage).ToList();
                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });
            return services;
        }
    }
}
