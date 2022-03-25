using System.Linq.Expressions;

namespace ERP.Domain.Core.Specifications
{
    public class WhereExpression<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
    }
}