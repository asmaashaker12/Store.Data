using StackExchange.Redis;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Order = Store.Data.Entities.OrderEntities.Order;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithPaymentIntendSpecification:BaseSpecifications<Order>
    {
        public OrderWithPaymentIntendSpecification(string paymentIntendId) : base(order=>order.PaymentIntendId==paymentIntendId)
        {

        }
    }
}
