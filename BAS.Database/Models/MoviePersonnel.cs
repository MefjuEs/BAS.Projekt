using BAS.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class MoviePersonnel
    {
        public long MovieId { get; set; }
        public long PersonId { get; set; }
        public FilmCrew MemberPosition { get; set; }
    }
}
