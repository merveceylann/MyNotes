using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    public class NotlarimUser:MyEntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivateGuid { get; set; } //globaluserid 16 haneli benzersiz bir kod cikariyor
        public bool IsAdmin { get; set; }
    }
}
