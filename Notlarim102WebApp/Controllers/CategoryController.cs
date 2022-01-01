﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using Notlarim102WebApp.Models;

namespace Notlarim102WebApp.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager();
        public ActionResult Index()
        {
            return View(CacheHelper.GetCategoriesFromCache());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cm.Find(s=>s.Id==id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                //    db.Categories.Add(category);
                //    db.SaveChanges();

                cm.Insert(category);
                CacheHelper.RemoveCatFromCache();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cm.Find(s=>s.Id==id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(category).State = EntityState.Modified;
                //db.SaveChanges();

                Category cat = cm.Find(s => s.Id == category.Id);
                cat.Title = category.Title;
                cat.Description = category.Description;
                
                cm.Update(cat);
                CacheHelper.RemoveCatFromCache();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cm.Find(s=>s.Id==id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category =cm.Find(s=>s.Id==id);
            cm.Delete(category);
            //db.Categories.Remove(category);
            //db.SaveChanges();
            CacheHelper.RemoveCatFromCache();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
