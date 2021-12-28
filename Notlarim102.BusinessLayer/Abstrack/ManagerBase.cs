using Notlarim102.Core.DataAccess;
using Notlarim102.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer.Abstrack
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class //generic bir soyut ve idataaccessten implemente edilebilcek
    {
        //araci
        //butun oucliclerin yanina virtual ekledik. yeri geldiginde ezebilmek icin
        Repostory<T> repo = new Repostory<T>();

        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public virtual T Find(Expression<Func<T, bool>> eresult)
        {
            return repo.Find(eresult);
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> eresult)
        {
            return repo.List(eresult);
        }

        public virtual IQueryable<T> QList()
        {
            return repo.QList();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
