using Backstage.Models;
using Backstage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Backstage.Repositories;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace Backstage.Services
{
    public class AlbumService
    {
        string connString = ConfigurationManager.ConnectionStrings["SoulfulBackStage"].ToString();
        public List<AlbumViewModel> GetModalinform()
        {
            SoulfulBackStage context = new SoulfulBackStage();
            SoulfulRepository<Singer> SingerRepository = new SoulfulRepository<Singer>(context);
            SoulfulRepository<Album> AlbumRepository = new SoulfulRepository<Album>(context);

            var albumContext = from Album in AlbumRepository.GetAll()
                               join Singer in SingerRepository.GetAll()
                                on Album.Singer_id equals Singer.Singer_id
                               select new AlbumViewModel
                               {
                                   Album_Name = Album.Album_Name,
                                   About = Album.About,
                                   Pic = Album.Pic,
                                   Singer = Singer.Name,
                                   Singer_id = Singer.Singer_id,
                                   Company = Album.Company,
                                   Datetime = Album.Datetime,
                                   Album_id = Album.Album_id,
                                   Price = Album.Price,
                                   Hits = Album.Hits,
                                   WeekHits = Album.WeekHits,
                                   MonthHits = Album.MonthHits
                               };
            return albumContext.ToList();
        }

        public List<HitViewModel> GetHit()
        {
            SoulfulBackStage context = new SoulfulBackStage();
            SoulfulRepository<Album> AlbumRepository = new SoulfulRepository<Album>(context);

            var albumContext = from Album in AlbumRepository.GetAll().OrderBy(x => x.Album_Name)
                               select new HitViewModel
                               {
                                   Name = Album.Album_Name,
                                   Hits = Album.Hits
                               };
            return albumContext.ToList();
        }

       

        public List<DetailViewModel> GetTotalAmount()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "SELECT " +
                             "Album_Name," +
                             "SUM(od.Count * od.Price) AS TotalAmount " +
                             "FROM Album a " +
                             "INNER JOIN OrderDetail od ON od.Album_id = a.Album_id " +
                             "GROUP BY Album_Name";

                List<DetailViewModel> orderDetails = conn.Query<DetailViewModel>(sql).ToList();

                return orderDetails;
            }
        }
        public int GetMembersCount()
        {         
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "SELECT " +
                             "COUNT(Id) AS UserCount " +
                             "FROM AspNetUsers ";
                var members = conn.Query<int>(sql).FirstOrDefault();

                return members;
            }
        }

        public int GetProductsCount()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "SELECT " +
                             "SUM(od.Count) AS TotalProducts " +
                             "FROM OrderDetail od";                        
                var products = conn.Query<int>(sql).FirstOrDefault();

                return products;
            }
        }

        public int GetTotal()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "SELECT " +
                             "SUM(od.Count*od.Price) AS Total " +
                             "FROM OrderDetail od";
                var total = conn.Query<int>(sql).FirstOrDefault();

                return total;
            }
        }





    }
}