using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class Personnel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<MoviePersonnel> Movies { get; set; }
    }
}
