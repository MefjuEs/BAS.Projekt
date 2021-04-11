namespace BAS.AppServices
{
    public class ReviewFilters
    {
        public long Id { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
