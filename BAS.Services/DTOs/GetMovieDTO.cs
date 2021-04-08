namespace BAS.AppServices
{
    public class GetMovieDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int MovieLengthInMinutes { get; set; }
        public byte MoviePoster { get; set; }
    }
}
