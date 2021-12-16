using Notlarim102.DataAccessLayer;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class Test
    {
         Repostory<NotlarimUser> ruser = new Repostory<NotlarimUser>();
         Repostory<Category> rcat = new Repostory<Category>();
         Repostory<Note> rnote = new Repostory<Note>();
         Repostory<Comment> rcom = new Repostory<Comment>();
         Repostory<Liked> rliked = new Repostory<Liked>();

        //Repostory<NotlarimUser> ruser = new Repostory<NotlarimUser>();
        public Test()
        {
            var test = rcat.List();
            var test1 = rcat.List(x=>x.Id>5);
            NotlarimContext db = new NotlarimContext();
            db.Categories.ToList();
            //db.Database.CreateIfNotExists(); seed metodunu calistirmadan database olusturur.
        }
        public void InsertTest()
        {
            int result = ruser.Insert(new NotlarimUser()
            {
                Name="Kerem",
                Surname="Ceylan",
                Email="kerem@gmail.com",
                ActivateGuid=Guid.NewGuid(),
                IsActive=true,
                IsAdmin=false,
                Username="keremceylan",
                Password="55",
                CreatdOn=DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUserName="keremceylan",
            });
        }

        public void UpdateTest()
        {
            NotlarimUser user = ruser.Find(x => x.Username == "keremceylan");
            if (user!=null)
            {
                user.Password = "5534";
                ruser.Update(user);
            }
        }
        public void DeleteTest()
        {
            NotlarimUser user = ruser.Find(x => x.Username == "keremceylan");
            if (user != null)
            {
                user.Password = "1111111";
                ruser.Delete(user);
            }
        }

        public void CommentTest()
        {
            NotlarimUser user = ruser.Find(s => s.Id == 1);
            Note note = rnote.Find(s => s.Id == 3);
            Comment comment = new Comment()
            {
                Text = "Bu bir test datasidir.",
                CreatdOn = DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUserName="merveceylan",
                Note= note,
                Owner= user
            };
            rcom.Insert(comment);
        }
    }
}
