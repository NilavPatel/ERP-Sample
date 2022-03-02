using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles
{
    public class GetAllRolesReq : IRequest<GetAllRolesRes>
    {
        public string SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllRolesRes
    {
        public IList<Role> Result { get; set; }
        public int Count { get; set; }
    }

    public class GetRoleById : IRequest<Role>
    {
        public Guid Id { get; set; }
    }
}