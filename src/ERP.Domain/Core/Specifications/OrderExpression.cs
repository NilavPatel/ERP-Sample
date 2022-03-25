using System.Linq.Expressions;

namespace ERP.Domain.Core.Specifications
{
    public class OrderExpression<T>
    {
        public Expression<Func<T, object>> KeySelector { get; set; }
        public OrderTypeEnum OrderType { get; set; }
    }
}