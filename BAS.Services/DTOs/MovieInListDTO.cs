using System.Collections.Generic;

namespace BAS.AppServices
{
    public class MovieInListDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public byte[] Poster { get; set; }
        public List<string> Genres { get; set; }
    }
}
