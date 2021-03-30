using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class Review
    {
        public long UserId { get; set; }
        public long MovieId { get; set; }
        [Required]
        public double Rating { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
