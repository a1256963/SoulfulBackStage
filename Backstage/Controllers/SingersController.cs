using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backstage.Models;

namespace Backstage.Controllers
{
    public class SingersController : Controller
    {
        private SoulfulBackStage db = new SoulfulBackStage();
        [Authorize(Users = "john@gmail.com")]
        // GET: Singers
        public ActionResult Index(string searching)
        {
            var singer = db.Singer.Include(s => s.Language);
            return View(db.Singer.Where(x=>x.Language.Language_type.Contains(searching)||x.Name.Contains(searching)||x.Gender.Contains(searching)||x.Country.Contains(searching)||searching==null).ToList());
        }

        // GET: Singers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singer.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // GET: Singers/Create
        public ActionResult Create()
        {
            ViewBag.Language_id = new SelectList(db.Language, "Language_id", "Language_type");
            return View();
        }

        // POST: Singers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Singer_id,Name,Gender,Country,Language_id")] Singer singer)
        {
            if (ModelState.IsValid)
            {
                db.Singer.Add(singer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Language_id = new SelectList(db.Language, "Language_id", "Language_type", singer.Language_id);
            return View(singer);
        }

        // GET: Singers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singer.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Language_id = new SelectList(db.Language, "Language_id", "Language_type", singer.Language_id);
            return View(singer);
        }

        // POST: Singers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Singer_id,Name,Gender,Country,Language_id")] Singer singer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(singer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Language_id = new SelectList(db.Language, "Language_id", "Language_type", singer.Language_id);
            return View(singer);
        }

        // GET: Singers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singer.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // POST: Singers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Singer singer = db.Singer.Find(id);
            db.Singer.Remove(singer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
