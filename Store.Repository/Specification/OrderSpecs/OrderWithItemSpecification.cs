using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithItemSpecification : BaseSpecifications<Order>
    {
        public OrderWithItemSpecification(string buyerEmail) : base(order=>order.EmailBuyer==buyerEmail)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.OrderItems);
            AddOrerByDescinding(x => x.orderDate);
        }

        public OrderWithItemSpecification(int id) : base(order =>order.Id == id)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.OrderItems);
        }
    }
}
