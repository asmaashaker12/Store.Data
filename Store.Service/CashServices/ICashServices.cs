using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.CashServices
{
    public  interface ICashServices
    {
        Task SetCashResponseAsync(string key, object response, TimeSpan timetolive);
        Task<string> GetCashResponseAsync(string key);
    }
}
