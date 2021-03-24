using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.DTOs
{
    public class RegisterResultDTO
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public bool Success { get; set; }
    }
}
