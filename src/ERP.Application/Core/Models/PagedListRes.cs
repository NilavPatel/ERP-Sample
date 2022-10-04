namespace ERP.Application.Core.Models
{
    public class PagedListRes<T>
    {
        public IList<T>? Result { get; set; }
        public int Count { get; set; }
    }
}