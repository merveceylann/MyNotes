using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notlarim102.Entity.ValueObject
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanici Adi"), Required(ErrorMessage = "{0} alani bos gecilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmali.")]
        public string Username { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} alani bos gecilemez."), StringLength(100, ErrorMessage = "{0} max. {1} karakter olmali."), EmailAddress(ErrorMessage = "{0} alani icin gecerli bir Email giriniz.")]
        public string Email { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani bos gecilemez."), StringLength(30, MinimumLength =3, ErrorMessage = "{0} max. {1} ile min. {2} karakter olmali.")]
        public string Password { get; set; }
        [DisplayName("Sifre Tekrar"), Required(ErrorMessage = "{0} alani bos gecilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmali."), Compare("Password",ErrorMessage ="{0} ile {1} uyusmuyor")]
        public string RePassword { get; set; }
    }
}