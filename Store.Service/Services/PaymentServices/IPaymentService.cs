using Store.Service.OrderServices.OrderDtos;
using Store.Service.Services.BasketService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto>CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string PaymentIntenId);
        Task<OrderDetailsDto> UpdateOrderPaymentFailed(string PaymentIntenId);
    }
}
