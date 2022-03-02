using ERP.Domain.Modules.Designations;
using MediatR;

namespace ERP.Application.Modules.Designations
{
    public class GetAllDesignationsReq : IRequest<GetAllDesignationsRes>
    {
        public string SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllDesignationsRes
    {
        public IList<Designation> Result { get; set; }
        public int Count { get; set; }
    }

    public class GetDesignationById : IRequest<Designation>
    {
        public Guid Id { get; set; }
    }
}