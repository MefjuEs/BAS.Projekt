using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.DTOs
{
    public class MovieInListDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public byte[] Poster { get; set; }
        public List<string> Genres { get; set; }
    }
}
