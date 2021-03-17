using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Poster { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string MovieLength { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public double AverageRating { get; set; }

        public List<MoviePersonnel> Crew { get; set; }
        public List<MovieGenre> Genres { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
