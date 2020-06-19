using Backstage.Services;
using Backstage.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;


namespace Backstage.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Users = "john@gmail.com")]
        public ActionResult Index()
        {
            AlbumService albumService = new AlbumService();

            double sum = 0;
            var array = new List<double>();
            foreach (var m in albumService.GetHit())
            {
                sum += m.Hits;
            }

            foreach (var m in albumService.GetHit())
            {
                var value = Math.Floor(((m.Hits) / sum) * 100);
                array.Add(value);
            }
            ViewData["Hit"] = albumService.GetHit();
            ViewData["Value"] = array;
           
            ViewData["Members"] = albumService.GetMembersCount();
            ViewData["Products"] = albumService.GetProductsCount();
            ViewData["Total"] = albumService.GetTotal();

            return View(albumService.GetTotalAmount());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}