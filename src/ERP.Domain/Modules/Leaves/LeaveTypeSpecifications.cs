using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Leaves
{
    public class LeaveTypeSpecifications
    {
        public static BaseSpecification<LeaveType> GetLeaveTypeByIdSpec(Guid id)
        {
            return new BaseSpecification<LeaveType>(x => x.Id == id);
        }

        public static BaseSpecification<LeaveType> GetAllLeaveTypesSpec()
        {
            var spec = new BaseSpecification<LeaveType>();
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<LeaveType> SearchLeaveTypesSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<LeaveType>(x => (x.Name.Contains(searchKeyword)
                     || x.Description.Contains(searchKeyword)));
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<LeaveType> GetByLeaveTypeNameSpec(string name)
        {
            return new BaseSpecification<LeaveType>(x => x.Name == name);
        }

        public static BaseSpecification<LeaveType> GetAllActiveLeaveTypesSpec()
        {
            var spec = new BaseSpecification<LeaveType>(x => x.IsActive);
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }
    }
}