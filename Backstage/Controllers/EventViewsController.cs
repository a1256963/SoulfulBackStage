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
    public class EventViewsController : Controller
    {
        private SoulfulBackStage db = new SoulfulBackStage();
        [Authorize(Users = "john@gmail.com")]
        // GET: EventViews
        //x => x.Name.Contains(searching) || x.Pic.Contains(searching) ||x.Singer.Name.Contains(searching)||x.Adress.Contains(searching)||x.About.Contains(searching)|| searching == null
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
            var workers = from w in db.EventView
                          select w;
            if (!string.IsNullOrEmpty(searchString))
            {
                workers = workers.Where(w => w.Name.Contains(searchString)
                || w.Singer.Name.Contains(searchString)||w.Adress.Contains(searchString)||w.About.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "first_desc":
                    workers = workers.OrderByDescending(w => w.Name);
                    break;
                case "last_desc":
                    workers = workers.OrderByDescending(w => w.Singer.Name);
                    break;
                case "last":
                    workers = workers.OrderBy(w => w.Singer.Name);
                    break;
                default:
                    workers = workers.OrderBy(w => w.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workers.ToPagedList(pageNumber, pageSize));
        }

        // GET: EventViews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventView eventView = db.EventView.Find(id);
            if (eventView == null)
            {
                return HttpNotFound();
            }
            return View(eventView);
        }

        // GET: EventViews/Create
        public ActionResult Create()
        {
            ViewBag.Singer_id = new SelectList(db.Singer, "Singer_id", "Name");
            return View();
        }

        // POST: EventViews/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_id,Name,Singer_id,Datetime,Pic,Adress,About")] EventView eventView)
        {
            if (ModelState.IsValid)
            {
                db.EventView.Add(eventView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Singer_id = new SelectList(db.Singer, "Singer_id", "Name", eventView.Singer_id);
            return View(eventView);
        }

        // GET: EventViews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventView eventView = db.EventView.Find(id);
            if (eventView == null)
            {
                return HttpNotFound();
            }
            ViewBag.Singer_id = new SelectList(db.Singer, "Singer_id", "Name", eventView.Singer_id);
            return View(eventView);
        }

        // POST: EventViews/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_id,Name,Singer_id,Datetime,Pic,Adress,About")] EventView eventView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Singer_id = new SelectList(db.Singer, "Singer_id", "Name", eventView.Singer_id);
            return View(eventView);
        }

        // GET: EventViews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventView eventView = db.EventView.Find(id);
            if (eventView == null)
            {
                return HttpNotFound();
            }
            return View(eventView);
        }

        // POST: EventViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventView eventView = db.EventView.Find(id);
            db.EventView.Remove(eventView);
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
