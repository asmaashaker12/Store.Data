using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class BaseSpecifications<T> : ISpecifcation<T>
    {
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<System.Linq.Expressions.Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        //    Expression<Func<T, bool>> ISpecifcation<T>.Criteria => throw new NotImplementedException();

        //   List<Expression<Func<T, object>>> ISpecifcation<T>.Includes => throw new NotImplementedException();

        public Expression<Func<T, object>> OrderBy { get; private set; }

       public Expression<Func<T, object>>OrderByDescinding { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T,object>> includeExpression)
            =>Includes.Add(includeExpression);
        protected void AddOrerBy(Expression<Func<T, object>> OrderByExpression)
            => OrderBy = OrderByExpression;
        protected void AddOrerByDescinding(Expression<Func<T, object>> OrderByExpressionDescinding)
            => OrderBy = OrderByExpressionDescinding;
        protected void ApplyPagination(int skip,int take)
        {
            Take = take;
            Skip=skip;
            IsPaginated = true;

        }
    }
}
