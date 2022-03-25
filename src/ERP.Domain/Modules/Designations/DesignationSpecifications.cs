using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Designations
{
    public class DesignationSpecifications
    {
        public static BaseSpecification<Designation> GetDesignationByIdSpec(Guid id)
        {
            return new BaseSpecification<Designation>(x => x.Id == id);
        }

        public static BaseSpecification<Designation> GetAllDesignationsSpec()
        {
            var spec = new BaseSpecification<Designation>();
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Designation> SearchDesignationsSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<Designation>(x => (x.Name.Contains(searchKeyword)
                    || x.Description.Contains(searchKeyword)));
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Designation> GetByDesignationNameSpec(string name)
        {
            return new BaseSpecification<Designation>(x => x.Name == name);
        }

    }
}