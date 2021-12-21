using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using Notlarim102.Entity.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class NotlarimUserManager
    {
        private Repostory<NotlarimUser> ruser = new Repostory<NotlarimUser>();

        //home cont. yaptigimiz hata gosterme islemini burada yapacagiz.
        //database ile karsilastircaz
        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = ruser.Find(s => s.Username == data.Username || s.Email == data.Email);

            BusinessLayerResult<NotlarimUser> layerResult = new BusinessLayerResult<NotlarimUser>();
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.Errors.Add("Kullanici adi daha once kaydedilmis");
                }
                if (user.Email == data.Email) //else diyemeyiz cunku biri digerinin on kosulu degil
                {
                    layerResult.Errors.Add("Email daha once kullanilmis");
                }

                //hata firlatma teknigini kullanabiliriz.

                //throw new Exception("Bu bilgiler daha once kullanilmis!");

                //try catchini home cont. yazdik
            }
            else
            {
                DateTime now = DateTime.Now;
                int dbResult = ruser.Insert(new NotlarimUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    ModifiedOn = now,
                    CreatdOn = now,
                    ModifiedUserName = "system",
                });
                if (dbResult > 0)
                {
                    layerResult.Result = ruser.Find(s => s.Email == data.Email && s.Username == data.Username);
                }
            }
            return layerResult;
        }

        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giris kontrolu
            //hesap aktif mi kontrolu

            //controllerda yapilacaklar
            //yonlendirme
            //session a kullanici bilgilerini gonderme 

            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(s => s.Username == data.Username && s.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive) //varsayalani true oldugu icin false ise iceri girecek acemice == yapmadik :D
                {
                    res.Errors.Add("Kullanici aktiflestirilmemis. Lutfen mailinizi kontrol edin");
                }
            }
            else
            {
                res.Errors.Add("Kullanici adi ya da sifre yanlis");
            }
            return res;
        }
    }
}
