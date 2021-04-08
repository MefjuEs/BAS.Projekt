namespace BAS.AppServices
{
    public class ReviewDTO
    {
        public long UserId { get; set; }
        public long MovieId { get; set; }
        public string Message { get; set; }
        public double Rating { get; set; }
    }
}
