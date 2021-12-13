using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    public class MyEntityBase
    {
        public int Id { get; set; }
        public DateTime CreatdOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedUserName { get; set; }
    }
}
