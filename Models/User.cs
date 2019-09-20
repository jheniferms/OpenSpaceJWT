using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpace.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
    }
}
