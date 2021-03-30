using BAS.AppCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class MoviePersonnel
    {
        public long MovieId { get; set; }
        public long PersonId { get; set; }
        [Required]
        public FilmCrew MemberPosition { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        [ForeignKey("PersonId")]
        public virtual Personnel Personnel { get; set; }
    }
}
