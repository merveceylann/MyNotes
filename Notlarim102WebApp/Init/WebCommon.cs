using Notlarim102.Common;
using Notlarim102.Entity;
using Notlarim102WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102WebApp.Init
{
    public class WebCommon : Icommon
    {
        public string GetCurrentUsername()
        {
            //if (HttpContext.Current.Session["login"]!=null)
            if (CurrentSession.User!=null)
            {
                NotlarimUser user = CurrentSession.User as NotlarimUser;
                return user.Username;
            }
            return "system";
        }
    }
}