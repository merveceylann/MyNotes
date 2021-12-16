using Notlarim102.DataAccessLayer;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    public class CategoryControl
    {
        private NotlarimContext db = new NotlarimContext();

        public void Insert(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
        }
    }
    public class NoteControl
    {
        private NotlarimContext db = new NotlarimContext();

        public void Insert(Note obj)
        {
            db.Notes.Add(obj);
            db.SaveChanges();
        }
    }
    //T nesnesi Typetan geliyo
    public class Repo<T> where T:class  //typeın class olmasını sagla
    {
        private NotlarimContext db = new NotlarimContext();

        public void Insert(T obj)
        {
            db.Set<T>().Add(obj);  //buranin beklentisi dbcontext o yuzden where kosulu yazcaz
            db.SaveChanges();
        }
    }
}
