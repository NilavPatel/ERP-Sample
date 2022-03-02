using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Designations
{
    public class DesignationSpecifications
    {
        public static BaseSpecification<Designation> GetDesignationByIdSpec(Guid id)
        {
            return new BaseSpecification<Designation>(x => x.Id == id && x.IsDeleted == false);
        }

        public static BaseSpecification<Designation> GetAllDesignationsSpec()
        {
            return new BaseSpecification<Designation>(x => x.IsDeleted == false);
        }

        public static BaseSpecification<Designation> SearchDesignationsSpec(string searchKeyword)
        {
            return new BaseSpecification<Designation>(x => (x.Name.Contains(searchKeyword)
                    || x.Description.Contains(searchKeyword))
                && x.IsDeleted == false);
        }

        public static BaseSpecification<Designation> GetByDesignationNameSpec(string name)
        {
            return new BaseSpecification<Designation>(x => x.Name == name 
                && x.IsDeleted == false);
        }

    }
}