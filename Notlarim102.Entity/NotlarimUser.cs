using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblNotlarimUsers")]
    public class NotlarimUser:MyEntityBase
    {
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string Surname { get; set; }
        [StringLength(30)]
        public string Username { get; set; }
        [StringLength(100),Required]
        public string Email { get; set; }
        [StringLength(100),Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; } //globaluserid 16 haneli benzersiz bir kod cikariyor
        public bool IsAdmin { get; set; }

        public virtual List<Note> Notes { get; set; } //bir kullanicinin birden cok notu olabilir
        public virtual List<Comment> Comments { get; set; } //bir kullanicinin birden cok yorumu olabilir
        public virtual List<Liked> Likes { get; set; } //bir kullanicinin birden cok begenisi olabilir
    }
}
