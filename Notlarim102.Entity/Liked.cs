using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblLiked")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        //[ForeingKey]
        //public int NoteId { get; set; }
        //public int NotlarimUserId { get; set; }

        public virtual Note Note { get; set; }
        public virtual NotlarimUser LikedUser { get; set; } //basina virtual koyuyoruz ki burada idleri olussun

    }
}
