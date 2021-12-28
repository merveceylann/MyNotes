using Notlarim102.BusinessLayer.Abstrack;
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
    public class NotlarimUserManager:ManagerBase<NotlarimUser>
    {
        //her yerde kullanmisiz o yuzden globale aldik
        //refactor
        readonly BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();

        //private Repostory<NotlarimUser> ruser = new Repostory<NotlarimUser>();

        //home cont. yaptigimiz hata gosterme islemini burada yapacagiz.
        //database ile karsilastircaz
        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = Find(s => s.Username == data.Username || s.Email == data.Email);

            // BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanici adi daha once kaydedilmis");
                }
                if (user.Email == data.Email) //else diyemeyiz cunku biri digerinin on kosulu degil
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Email daha once kullanilmis");
                }

                //hata firlatma teknigini kullanabiliriz.

                //throw new Exception("Bu bilgiler daha once kullanilmis!");

                //try catchini home cont. yazdik
            }
            else
            {
                DateTime now = DateTime.Now;
                int dbResult = Insert(new NotlarimUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    ProfileImageFilename = "user1.jpeg"
                    //kapatilanlar repositoryde otomatik eklencek sekilde duzenlenecektir
                    //ModifiedOn = now,
                    //CreatdOn = now,
                    //ModifiedUserName = "system",
                });
                if (dbResult > 0)
                {
                    res.Result = Find(s => s.Email == data.Email && s.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activeUri = $"{siteUri}/Home/UserActivete/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br> Hesabinizi aktiflestirmek icin <a href='{activeUri}' target='_blank'> Tiklayiniz </a>.";
                    MailHelper.SendMail(body, res.Result.Email, "Notlarim102 hesap aktiflestirme");

                }
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giris kontrolu
            //hesap aktif mi kontrolu

            //controllerda yapilacaklar
            //yonlendirme
            //session a kullanici bilgilerini gonderme 

            //BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = Find(s => s.Username == data.Username && s.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive) //varsayalani true oldugu icin false ise iceri girecek acemice == yapmadik :D
                {
                  res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanici aktiflestirilmemis.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lutfen mailinizi kontrol edin");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanici adi ya da sifre yanlis");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> ActiveUser(Guid id)
        {
            //BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = Find(x => x.ActivateGuid == id);
            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Bu hesap daha once aktif edilmistir.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActiveIdDoesNotExist, "Hatali islem!!!");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> GetUserById(int id)
        {
            //BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = Find(s => s.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanici bulunamadi.");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> UpdateProfile(NotlarimUser data)
        {
            NotlarimUser user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            //BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            if (user != null && user.Id != data.Id)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu kullanici adi daha once kaydedilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu e-posta daha once kullanilmis.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            //if (string.IsNullOrEmpty(data.ProfileImageFilename)==false)
            if (!string.IsNullOrEmpty(data.ProfileImageFilename))
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }
            if (Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdate, "Profil guncellenemedi");
            }
            return res;

        }

        public BusinessLayerResult<NotlarimUser> DeleteProfile(int id)
        {

            NotlarimUser user = Find(x => x.Id ==id);
            //BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            if (user!=null)
            {
                if (Delete(user)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanici Silinemedi.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanici bulunamadi.");
            }
            return res;
        }
    }
}
