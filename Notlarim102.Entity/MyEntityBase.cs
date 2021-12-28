using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Oluşturulma Tarihi"), Required]
        public DateTime CreatdOn { get; set; }
        [DisplayName("Düzenlenme Tarihi"), Required]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Düzenleyen Kullanıcı"), Required, StringLength(30)]
        public string ModifiedUserName { get; set; }
    }
}
