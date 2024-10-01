using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.CashServices;
using System.Text;

namespace Store.Web.Helper
{
    public class CashAttribute:Attribute,IAsyncActionFilter
    {
        private readonly int _timeToLiveinSeconds;

        public CashAttribute(int timeToLiveinSeconds)
        {
            _timeToLiveinSeconds = timeToLiveinSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cashService = context.HttpContext.RequestServices.GetRequiredService<ICashServices>();
            var cashkey=GenerateCashKeyFromRequest(context.HttpContext.Request);
            var cashresponse = await _cashService.GetCashResponseAsync(cashkey);
            if (!string.IsNullOrEmpty(cashresponse))
            {
                var contentresult = new ContentResult()
                {
                    Content = cashresponse,
                    ContentType = "Application.json",
                    StatusCode = 200
                };
                context.Result= contentresult;
                return;
            }
            var executedcontext = await next();
            if (executedcontext.Result is OkObjectResult response)
                await _cashService.SetCashResponseAsync(cashkey, response.Value, TimeSpan.FromSeconds(_timeToLiveinSeconds));


        }
        private string GenerateCashKeyFromRequest(HttpRequest request) 
        {
            StringBuilder cashkey=new StringBuilder();
            cashkey.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                cashkey.Append($"|{ key}_{value}");

            }
            return cashkey.ToString();
        }
    }
}
