using BAS.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.DTOs
{
    public class InsertMovieCrewDTO
    {
        public long PersonnelId { get; set; }
        public FilmCrew FilmCrew { get; set; }
    }
}
