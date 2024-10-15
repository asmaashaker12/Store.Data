using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.PaymentServices;
using Stripe;

namespace Store.Web.Controllers
{
  
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        string endpointsecretes= "whsec_84b498d2636a599dee8ebf9ad9818d87018d01978be68e775c595c90646045c0";

        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> reateOrUpdatePaymentIntent(CustomerBasketDto input)
        =>Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));
        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],endpointsecretes);

                // Handle the event
                if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed :",paymentIntent.Id);
                 var order=   await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Orederd Failed :", order.BasketId);

                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                // Handle the event
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Sucess :", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("Orederd Success :", order.BasketId);
                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentCreated)
                {
                    _logger.LogInformation("Payment Created :");

                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }
}
