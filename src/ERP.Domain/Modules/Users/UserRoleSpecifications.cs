using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Users
{
    public static class UserRoleSpecifications
    {
        public static BaseSpecification<UserRole> GetByUserId(Guid userId)
        {
            var spec = new BaseSpecification<UserRole>(x => x.UserId == userId);
            return spec;
        }
    }
}