using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblComments")]
    public class Comment : MyEntityBase
    {
        [StringLength(3000), Required]
        public string Text { get; set; }

        public virtual Note Note { get; set; } //bir notun bir yorumu olur
        public virtual NotlarimUser Owner { get; set; } //bir yorumun bir sahibi olur
    }
}
