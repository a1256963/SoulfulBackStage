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
    public class GiveBacksController : Controller
    {
        private SoulfulBackStage db = new SoulfulBackStage();
        [Authorize(Users = "john@gmail.com")]
        // GET: GiveBacks
        public ActionResult Index(string searching)
        {
            return View(db.GiveBacks.Where(x=>x.Name.Contains(searching)||x.Email.Contains(searching)||x.Subject.Contains(searching)||x.Message.Contains(searching)||x.Status.Contains(searching)|| searching==null).ToList());
        }

        // GET: GiveBacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiveBacks giveBacks = db.GiveBacks.Find(id);
            if (giveBacks == null)
            {
                return HttpNotFound();
            }
            return View(giveBacks);
        }

        // GET: GiveBacks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiveBacks/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Subject,Message")] GiveBacks giveBacks)
        {
            if (ModelState.IsValid)
            {
                db.GiveBacks.Add(giveBacks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giveBacks);
        }

        // GET: GiveBacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiveBacks giveBacks = db.GiveBacks.Find(id);
            if (giveBacks == null)
            {
                return HttpNotFound();
            }
            return View(giveBacks);
        }

        // POST: GiveBacks/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Subject,Message,Status")] GiveBacks giveBacks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giveBacks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giveBacks);
        }

        // GET: GiveBacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiveBacks giveBacks = db.GiveBacks.Find(id);
            if (giveBacks == null)
            {
                return HttpNotFound();
            }
            return View(giveBacks);
        }

        // POST: GiveBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiveBacks giveBacks = db.GiveBacks.Find(id);
            db.GiveBacks.Remove(giveBacks);
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
