using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backstage.Models;
using PagedList;

namespace Backstage.Controllers
{
    public class SingersController : Controller
    {
        private SoulfulBackStage db = new SoulfulBackStage();
        [Authorize(Users = "john@gmail.com")]
        // GET: Singers
        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "last" ? "last_desc" : "last";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var workers = from w in db.Singer
                          select w;
            if (!string.IsNullOrEmpty(searchString))
            {
                workers = workers.Where(w => w.Language.Language_type.Contains(searchString)
                || w.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "first_desc":
                    workers = workers.OrderByDescending(w => w.Language.Language_type);
                    break;
                case "last_desc":
                    workers = workers.OrderByDescending(w => w.Name);
                    break;
                case "last":
                    workers = workers.OrderBy(w => w.Language.Language_type);
                    break;
                default:
                    workers = workers.OrderBy(w => w.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workers.ToPagedList(pageNumber, pageSize));
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
