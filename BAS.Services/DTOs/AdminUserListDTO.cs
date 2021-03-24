using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.DTOs
{
    public class AdminUserListDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
    }
}
