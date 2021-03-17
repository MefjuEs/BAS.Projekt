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
        public string UserId { get; set; }
        public long MovieId { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
    }
}
