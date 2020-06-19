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
    public class StylesController : Controller
    {
        private SoulfulBackStage db = new SoulfulBackStage();
        [Authorize(Users = "john@gmail.com")]
        // GET: Styles
        //x.Album.Album_Name.Contains(searching)||x.Style_type.Contains(searching)||searching==null
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
            var workers = from w in db.Style
                          select w;
            if (!string.IsNullOrEmpty(searchString))
            {
                workers = workers.Where(w => w.Album.Album_Name.Contains(searchString)
                || w.Style_type.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "first_desc":
                    workers = workers.OrderByDescending(w => w.Album.Album_Name);
                    break;
                case "last_desc":
                    workers = workers.OrderByDescending(w => w.Style_type);
                    break;
                case "last":
                    workers = workers.OrderBy(w => w.Style_type);
                    break;
                default:
                    workers = workers.OrderBy(w => w.Album.Album_Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Styles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Style style = db.Style.Find(id);
            if (style == null)
            {
                return HttpNotFound();
            }
            return View(style);
        }

        // GET: Styles/Create
        public ActionResult Create()
        {
            ViewBag.Album_id = new SelectList(db.Album, "Album_id", "Album_Name");
            return View();
        }

        // POST: Styles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Style_id,Style_type,Album_id")] Style style)
        {
            if (ModelState.IsValid)
            {
                db.Style.Add(style);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Album_id = new SelectList(db.Album, "Album_id", "Album_Name", style.Album_id);
            return View(style);
        }

        // GET: Styles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Style style = db.Style.Find(id);
            if (style == null)
            {
                return HttpNotFound();
            }
            ViewBag.Album_id = new SelectList(db.Album, "Album_id", "Album_Name", style.Album_id);
            return View(style);
        }

        // POST: Styles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Style_id,Style_type,Album_id")] Style style)
        {
            if (ModelState.IsValid)
            {
                db.Entry(style).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Album_id = new SelectList(db.Album, "Album_id", "Album_Name", style.Album_id);
            return View(style);
        }

        // GET: Styles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Style style = db.Style.Find(id);
            if (style == null)
            {
                return HttpNotFound();
            }
            return View(style);
        }

        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Style style = db.Style.Find(id);
            db.Style.Remove(style);
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
