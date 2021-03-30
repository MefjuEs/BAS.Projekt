using BAS.AppCommon.StaticValues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class Genre
    {
        public long Id { get; set; }
        [MaxLength(StaticValues.GenreNameMaxLength)]
        [Required]
        public string Name { get; set; }
        [MaxLength(StaticValues.GenreDescriptionMaxLength)]
        public string Description { get; set; }
        public virtual List<MovieGenre> MovieGenres { get; set; }
    }
}
