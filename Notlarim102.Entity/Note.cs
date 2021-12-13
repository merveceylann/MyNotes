using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    public class Note:MyEntityBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDraft { get; set; } //note yazabilirim ama taslak olarak kalabilir. yayimlansin mi kismi
        public int LikeCount { get; set; }
    }
}
