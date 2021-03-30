using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class MovieGenre
    {
        public long MovieId { get; set; }
        public long GenreId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
