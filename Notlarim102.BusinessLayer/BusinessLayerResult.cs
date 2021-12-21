using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class BusinessLayerResult<T> where T:class //generic yapida (T class ya da referans type yapida olabilir)
    {
        public List<string> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<string>();
        }
    }
}
