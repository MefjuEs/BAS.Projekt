using System;

namespace BAS.AppServices
{
    public class PersonnelFilters
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
