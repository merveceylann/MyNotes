using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Common.Helper
{
    public class ConfigHelper
    {
        //public static string Get(string key)
        //{
        //    return ConfigurationManager.AppSettings[key];
        //}
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));

            //once changetype cevirdik (bizden iki parametre bekliyo t nin ne oldugunu bilmedigimiz icin typeof dedik). sonra objeye donusen seyi (T) ye casting ettik 
        }
    }
}
