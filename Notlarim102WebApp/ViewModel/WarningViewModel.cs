using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102WebApp.ViewModel
{
    public class WarningViewModel:NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Uyari";
        }
    }
}