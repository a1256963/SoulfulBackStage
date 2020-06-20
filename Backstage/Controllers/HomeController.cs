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

            //Hits
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

            //WeekHits
            double weeksum = 0;
            var week_array = new List<double>();
            foreach (var i in albumService.GetWeekHits())
            {
                weeksum += i.WeekHits;
            }

            foreach (var i in albumService.GetWeekHits())
            {
                var week_value = Math.Floor(((i.WeekHits) / weeksum) * 100);
                week_array.Add(week_value);
            }
            ViewData["WeekHit"] = albumService.GetWeekHits();
            ViewData["WeekValue"] = week_array;

            //MonthHits
            double monthsum = 0;
            var month_array = new List<double>();
            foreach (var j in albumService.GetMonthHits())
            {
                monthsum += j.MonthHits;
            }

            foreach (var j in albumService.GetMonthHits())
            {
                var month_value = Math.Floor(((j.MonthHits) / monthsum) * 100);
                month_array.Add(month_value);
            }
            ViewData["MonthHit"] = albumService.GetMonthHits();
            ViewData["MonthValue"] = month_array;


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