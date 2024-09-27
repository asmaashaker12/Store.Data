using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public interface ISpecifcation<T>
    {
     public  Expression<Func<T, bool>> Criteria { get; }
       public List<Expression<Func<T, object >>> Includes { get; }
        Expression<Func<T, object>> OrderBy {get; }
        Expression<Func<T, object>> OrderByDescinding {get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
