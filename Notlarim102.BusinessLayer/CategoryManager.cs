using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class CategoryManager
    {
        private Repostory<Category> rcat = new Repostory<Category>();

        public List<Category> GetCategories()
        {
            return rcat.List();
        }
        public Category GetCategoriesById(int id)
        {
            return rcat.Find(s=>s.Id==id);
        }
    }
}
