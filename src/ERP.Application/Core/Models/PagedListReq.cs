namespace ERP.Application.Core.Models
{
    public class PagedListReq
    {
        public string? SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}