using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService
{
    public class BasketService : IBasketService
    {

        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;


        public BasketService(IBasketRepository basketRepository,IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        
           => await _basketRepository.DeleteBasketAsync(basketId);
        

        public async Task<CustomerBasketDto> GetCustomerAsync(string basketId)

        {
            var basket=_basketRepository.GetCustomerAsync(basketId);
            if (basket == null)
                return new CustomerBasketDto();
            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }
        

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id is null)
                input.Id=GenerateRandomBasketId();
                //Generate BasktId
                var customermap=_mapper.Map<CustomerBasket>(input);
            var updateBasket=await _basketRepository.UpdateBasketAsync(customermap);
            var mappedUpdatedBasket=_mapper.Map<CustomerBasketDto>(updateBasket);
            return mappedUpdatedBasket;

        }
        private string GenerateRandomBasketId()
        {
            Random random= new Random();
            int randomDigit = random.Next(1000, 10000);
            return $"BS-{randomDigit}";

        }

    }
}
