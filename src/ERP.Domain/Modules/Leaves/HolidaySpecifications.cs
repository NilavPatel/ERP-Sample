
using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Leaves
{
    public class HolidaySpecifications
    {
        public static BaseSpecification<Holiday> GetAllHolidaysInYearSpec(int year)
        {
            var start = new DateTime(year, 1, 1).ToUniversalTime();
            var end = new DateTime(year, 12, 31).ToUniversalTime();
            var spec = new BaseSpecification<Holiday>(x => x.HolidayOn >= start
                && x.HolidayOn <= end);
            spec.ApplyOrderByDescending(x => x.HolidayOn);
            return spec;
        }

        public static BaseSpecification<Holiday> GetAllHolidaysSpec()
        {
            var spec = new BaseSpecification<Holiday>();
            spec.ApplyOrderByDescending(x => x.HolidayOn);
            return spec;
        }

        public static BaseSpecification<Holiday> GetHolidayByIdSpec(Guid id)
        {
            return new BaseSpecification<Holiday>(x => x.Id == id);
        }

        public static BaseSpecification<Holiday> GetHolidayByDateSpec(DateTimeOffset holidayOn)
        {
            return new BaseSpecification<Holiday>(x => x.HolidayOn == holidayOn);
        }

        public static BaseSpecification<Holiday> SearchHolidayByNameAndYearSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<Holiday>(x => x.Name.Contains(searchKeyword)
                || x.HolidayOn.Year.ToString() == searchKeyword);
            spec.ApplyOrderByDescending(x => x.HolidayOn);
            return spec;
        }
    }
}