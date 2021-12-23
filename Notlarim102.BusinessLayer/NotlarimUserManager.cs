using Notlarim102.Common.Helper;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using Notlarim102.Entity.Messages;
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
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExist,"Kullanici adi daha once kaydedilmis");
                }
                if (user.Email == data.Email) //else diyemeyiz cunku biri digerinin on kosulu degil
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExist,"Email daha once kullanilmis");
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
                    ProfileImageFilename="user1.jpeg"
                    //kapatilanlar repositoryde otomatik eklencek sekilde duzenlenecektir
                    //ModifiedOn = now,
                    //CreatdOn = now,
                    //ModifiedUserName = "system",
                });
                if (dbResult > 0)
                {
                    layerResult.Result = ruser.Find(s => s.Email == data.Email && s.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activeUri = $"{siteUri}/Home/UserActivete/{layerResult.Result.ActivateGuid}";
                    string body = $"Merhaba {layerResult.Result.Username};<br><br> Hesabinizi aktiflestirmek icin <a href='{activeUri}' target='_blank'> Tiklayiniz </a>.";
                    MailHelper.SendMail(body, layerResult.Result.Email, "Notlarim102 hesap aktiflestirme");
                        
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
                    res.AddError(ErrorMessageCode.UserIsNotActive,"Kullanici aktiflestirilmemis.");
                    res.AddError(ErrorMessageCode.CheckYourEmail,"Lutfen mailinizi kontrol edin");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong,"Kullanici adi ya da sifre yanlis");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> ActiveUser(Guid id)
        {
            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(x => x.ActivateGuid == id);
            if (res.Result!=null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Bu hesap daha once aktif edilmistir.");
                    return res;
                }
                res.Result.IsActive = true;
                ruser.Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActiveIdDoesNotExist, "Hatali islem!!!");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> GetUserById(int id)
        {
            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(s => s.Id == id);
            if (res.Result==null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanici bulunamadi.");
            }
            return res;
        }
    }
}
