﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblCategories")]
    public class Category : MyEntityBase
    {
        [DisplayName("Başlık"),StringLength(50), Required]
        public string Title { get; set; }
        [DisplayName("Açıklama"), StringLength(150)]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; } //fk

        public Category()
        {
            Notes = new List<Note>();
        }

    }
}
