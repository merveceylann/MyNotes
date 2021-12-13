using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblNotes")]
    public class Note:MyEntityBase
    {
        [StringLength(60),Required]
        public string Title { get; set; }
        [StringLength(2000),Required]
        public string Text { get; set; }
        public bool IsDraft { get; set; } //note yazabilirim ama taslak olarak kalabilir. yayimlansin mi kismi
        public int LikeCount { get; set; }

        public int CategoryId { get; set; }

        public virtual NotlarimUser Owner { get; set; }
        public virtual Category Category { get; set; } 
        public virtual List<Comment> Comments { get; set; }  //bir notun birden fazla yorum alir
        public virtual List<Liked> Likes { get; set; }  //bir notun birden fazla begenisi olabilir

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

        //db.note.tolist()
        //db.note.category.id - uzunn bir yol eskide kaldi
        //db.category.tolist()
        //db.note.tolist()
        //db.user.tolist()
    }
}
