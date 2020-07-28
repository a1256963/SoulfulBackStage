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
            foreach (var all in albumService.GetHit())
            {
                sum += all.Hits;
            }

            foreach (var all in albumService.GetHit())
            {
                var value = Math.Floor(((all.Hits) / sum) * 100);
                array.Add(value);
            }
            ViewData["Hit"] = albumService.GetHit();
            ViewData["Value"] = array;

            //WeekHits
            double weeksum = 0;
            var week_array = new List<double>();
            foreach (var week in albumService.GetWeekHits())
            {
                weeksum += week.WeekHits;
            }

            foreach (var week in albumService.GetWeekHits())
            {
                var week_value = Math.Floor(((week.WeekHits) / weeksum) * 100);
                week_array.Add(week_value);
            }
            ViewData["WeekHit"] = albumService.GetWeekHits();
            ViewData["WeekValue"] = week_array;

            //MonthHits
            double monthsum = 0;
            var month_array = new List<double>();
            foreach (var month in albumService.GetMonthHits())
            {
                monthsum += month.MonthHits;
            }

            foreach (var month in albumService.GetMonthHits())
            {
                var month_value = Math.Floor(((month.MonthHits) / monthsum) * 100);
                month_array.Add(month_value);
            }
            ViewData["MonthHit"] = albumService.GetMonthHits();
            ViewData["MonthValue"] = month_array;


            ViewData["Members"] = albumService.GetMembersCount();
            ViewData["Products"] = albumService.GetProductsCount();
            ViewData["Total"] = albumService.GetTotal();

            return View(albumService.GetThisMonth());
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