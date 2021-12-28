using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using Notlarim102.Entity.Messages;
using Notlarim102.Entity.ValueObject;
using Notlarim102WebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Notlarim102WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly NoteManager nm = new NoteManager();
        CategoryManager cm = new CategoryManager();
        NotlarimUserManager num = new NotlarimUserManager();
        BusinessLayerResult<NotlarimUser> res;


        // GET: Home
        public ActionResult Index()
        {
            Test test = new Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            //test.CommentTest();

            //if (TempData["mm"]!=null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}

            //NoteManager nm = new NoteManager();

            //return View(nm.GetAllNotes().OrderByDescending(x => x.ModifiedOn).ToList());
            return View(nm.QList().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //CategoryManager cm = new CategoryManager();
            //Category cat = cm.GetCategoriesById(id.Value);

            List<Note> notes = nm.QList().Where(x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(x => x.ModifiedOn).ToList();

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //}

            //TempData["mm"] = cat.Notes;

            //return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
            return View("Index", notes);
        }

        public ActionResult MostLiked()
        {
            //NoteManager nm = new NoteManager();
            //return View("Index", nm.GetAllNotes().OrderByDescending(x => x.LikeCount).ToList());            
            return View("Index", nm.QList().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //NotlarimUserManager num = new NotlarimUserManager();
                //BusinessLayerResult<NotlarimUser> res = num.LoginUser(model);
                res = num.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = $"https://localhost:44338/Home/UserActive/{res.Result.ActivateGuid}";
                    }
                    res.Errors.ForEach(s => ModelState.AddModelError("", s.Message));
                    return View();
                }
                Session["login"] = res.Result; //sessiona kullanici bilgileri gonderme

                return RedirectToAction("Index"); //yonlendirme
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //kullanici adinin uygunlugu unique
            //email kontrolu
            //aktivasyon islemi yapilmali
            //bool hasError = false;
            if (ModelState.IsValid)
            {
                //NotlarimUserManager num = new NotlarimUserManager();

                BusinessLayerResult<NotlarimUser> res = num.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(s => ModelState.AddModelError("", s.Message));
                    return View(model);
                }


                //NotlarimUser user = null;
                //try
                //{
                //    user = num.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}

                //kontrol eder. yani modelden gelen yapi uygunsa
                //if (model.Username=="aaa")
                //{
                //    ModelState.AddModelError("","Bu kullanici adi uygun degil!");
                //    //hasError = true;
                //}
                //if (model.Email=="aaa@aaa.com")
                //{
                //    ModelState.AddModelError("", "Email adresi daha once kullanilmis. Baska bir email deneyin.");
                //    //hasError = true;
                //}
                ////if (hasError==true)
                ////{
                ////    return View(model);
                ////}
                ////else
                ////{
                ////    return RedirectToAction("RegisterOk");
                ////}

                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}
                OkViewModel notifiyObj = new OkViewModel()
                {
                    Title = "Kayit Basarili",
                    RedirectingUrl = "/Home/Login"
                };
                notifiyObj.Items.Add("Lutfen e-posta adresinize gonderilen aktivasyon linkine tiklayarak hesabinizi aktif edin. Hesabinizi aktif etmeden not ekleyemez ve begenme yapamazsınız.");
                return View("Ok", notifiyObj);
            }

            return View(model);
        }
        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivete(Guid id)
        {
            //NotlarimUserManager num = new NotlarimUserManager();
            //BusinessLayerResult<NotlarimUser> res = num.ActiveUser(id);
            res = num.ActiveUser(id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifiyObj = new ErrorViewModel()
                {
                    Title = "Gecersiz Aktivasyon Islemi",
                    Items = res.Errors
                };
                return View("Error", errorNotifiyObj);
            }

            OkViewModel notifiyObj = new OkViewModel()
            {
                Title = "Hesap Aktiflestirildi.",
                RedirectingUrl = "/Home/Login"
            };
            notifiyObj.Items.Add("Hesabiniz aktiflestirildi. Artik not paylasimi yapabilirsiniz.");
            return View("Ok", notifiyObj);
            //return RedirectToAction("UserActiveteOk");


        }

        public ActionResult UserActiveteOk()
        {
            return View();
        }
        public ActionResult UserActiveteCancel()
        {
            List<ErrorMessageObject> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<ErrorMessageObject>;
            }
            return View(errors);
        }

        public ActionResult ShowProfile()
        {

            //NotlarimUserManager num = new NotlarimUserManager();
            //BusinessLayerResult<NotlarimUser> res = num.GetUserById(currentUser.Id);

            //NotlarimUser currentUser = Session["login"] as NotlarimUser;
            //if (currentUser != null) res = num.GetUserById(currentUser.Id);

            if (Session["login"] is NotlarimUser currentUser) res = num.GetUserById(currentUser.Id);
            //tek satira indi
            
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifiyObj = new ErrorViewModel()
                {
                    Title = "Gecersiz Profile Islemi",
                    Items = res.Errors
                };
                return View("Error", errorNotifiyObj);
            }
            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            //NotlarimUser currentUser = Session["login"] as NotlarimUser;
            //NotlarimUserManager num = new NotlarimUserManager();
            //BusinessLayerResult<NotlarimUser> res = num.GetUserById(currentUser.Id);
            // res = num.GetUserById(currentUser.Id);

            if (Session["login"] is NotlarimUser currentUser) res = num.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Olustu",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }
            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(NotlarimUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    //user_5.png
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }
                //NotlarimUserManager num = new NotlarimUserManager();
                //BusinessLayerResult<NotlarimUser> res = num.UpdateProfile(model);
                res = num.UpdateProfile(model);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Title = "Hata Olustu",
                        Items = res.Errors
                    };
                    return View("Error", errorNotifyObj);
                }
                Session["login"] = res.Result;
                return RedirectToAction("ShowProfile");
            }
            return View(model);
        }

        public ActionResult DeleteProfile()
        {
            //NotlarimUser currentUser = Session["login"] as NotlarimUser;
            //NotlarimUserManager num = new NotlarimUserManager();
            //BusinessLayerResult<NotlarimUser> res = num.DeleteProfile(currentUser.Id);
            //res = num.DeleteProfile(currentUser.Id);

            if (Session["login"] is NotlarimUser currentUser) res = num.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Olustu",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }
            Session.Clear();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult DeleteProfile(int id)
        //{
        //    return View();
        //}

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}