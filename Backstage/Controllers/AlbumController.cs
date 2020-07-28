using Backstage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backstage.Models;
using System.Data.Entity;

namespace Backstage.Controllers
{
    public class AlbumController : Controller
    {
        
        private SoulfulBackStage db = new SoulfulBackStage();
        [Authorize(Users = "john@gmail.com")]
        // GET: Album
        public ActionResult Albums()
        {
            AlbumService albumService = new AlbumService();

            return View(albumService.GetModalinform());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Album_id,Singer_id,Datetime,Album_Name,Pic,About,Company,Price")] Album albums)
        {
            if (ModelState.IsValid)
            {
                db.Album.Add(albums);
                db.SaveChanges();
                return RedirectToAction("Albums");
            }

            return View(albums);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album albums = db.Album.Find(id);
            if (albums == null)
            {
                return HttpNotFound();
            }
            return View(albums);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Album_id,Singer_id,Datetime,Album_Name,Pic,About,Company,Price,Hits,WeekHits,MonthHits")] Album albums)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albums).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Albums");
            }
            return View(albums);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album albums = db.Album.Find(id);
            if (albums == null)
            {
                return HttpNotFound();
            }
            return View(albums);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album albums = db.Album.Find(id);
            db.Album.Remove(albums);
            db.SaveChanges();
            return RedirectToAction("Albums");
        }
    }
}