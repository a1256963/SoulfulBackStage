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
            using (SqlConnection conn = new SqlConnection(@"data source=soulful2020.database.windows.net;initial catalog=soulful2020;persist security info=True;user id=soulful2020@soulful2020.database.windows.net;password=Soulful_2020;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                string sql = "select Album_Name,(Count * od.Price) as TotalAmount from OrderDetail od inner join Album a on od.Album_id=a.Album_id";
                List<DetailViewModel> orderDetails = conn.Query<DetailViewModel>(sql).ToList();

                AlbumService albumService = new AlbumService();


                double sum = 0;
                var array = new List<double>();
                foreach (var m in albumService.GetHit())
                {
                    sum += m.Hits;
                }

                foreach (var m in albumService.GetHit())
                {
                    //var value = Math.Round(((m.Hits) / sum) * 100, 2);
                    var value = Math.Floor(((m.Hits) / sum) * 100);
                    array.Add(value);
                }
                ViewData["Hit"] = albumService.GetHit();
                ViewData["Value"] = array;

                return View(orderDetails);
            }

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