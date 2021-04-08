using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.DTOs
{
    public class MovieFilters
    {
        public string Title { get; set; }
        public string Nationality { get; set; }
        public int ReleaseYearFrom { get; set; }
        public int ReleaseYearTo { get; set; }
        public int MovieLengthFrom { get; set; }
        public int MovieLengthTo { get; set; }
        public double AvgRatingFrom { get; set; }
        public double AvgRatingTo { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
