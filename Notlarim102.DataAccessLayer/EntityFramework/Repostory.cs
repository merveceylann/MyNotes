﻿using Notlarim102.DataAccessLayer;
using Notlarim102.DataAccessLayer.Abstrack;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    public class Repostory<T>:RepositoryBase,IRepository<T> where T : class
    {
        //once miraz sonra interface
        //private NotlarimContext db = new NotlarimContext();
        private NotlarimContext db;
        private DbSet<T> objSet;

        public Repostory()
        {
            db = RepositoryBase.CreateContext();
            objSet = db.Set<T>();
        }
        public List<T> List()
        {
            return objSet.ToList();
        }
        public List<T> List(Expression<Func<T, bool>> eresult)
        {
            //return db.Set<T>().ToList();
            //db.Categories.Where(x => x.Id == 5).ToList(); true ise sorgu sonucu dondurur. false ise dondurmez o yuzden yukarida bool tanimladik.

            return objSet.Where(eresult).ToList();
        }

        public int Insert(T obj)
        {
            objSet.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Delete(T obj)
        {
            objSet.Remove(obj);
            return Save();
        }

        //public int Save()
        //{
        //    return db.SaveChanges();
        //}
        //disaridan cagirmamak icin private yaptik.
        //bu bi teknik
        //private int Save()
        //{
        //    return db.SaveChanges();
        //} 
        public int Save()
        {
            return db.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> eresult)
        {
            return objSet.FirstOrDefault(eresult);
        }

        public IQueryable<T> listQueryable()
        {
            return objSet.AsQueryable<T>();
        }
    }
}