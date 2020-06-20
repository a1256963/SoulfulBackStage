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
                               orderby Album.Hits descending
                               select new HitViewModel
                               {
                                   Name = Album.Album_Name,
                                   Hits = Album.Hits
                               };
            return albumContext.Take(10).ToList();
        }

        public List<HitViewModel> GetWeekHits()
        {
            SoulfulBackStage context = new SoulfulBackStage();
            SoulfulRepository<Album> AlbumRepository = new SoulfulRepository<Album>(context);

            var albumContext = from Album in AlbumRepository.GetAll().OrderBy(x => x.Album_Name)
                               orderby Album.WeekHits descending
                               select new HitViewModel
                               {
                                   Name = Album.Album_Name,
                                   WeekHits = Album.WeekHits
                               };
            return albumContext.Take(10).ToList();
        }

        public List<HitViewModel> GetMonthHits()
        {
            SoulfulBackStage context = new SoulfulBackStage();
            SoulfulRepository<Album> AlbumRepository = new SoulfulRepository<Album>(context);

            var albumContext = from Album in AlbumRepository.GetAll().OrderBy(x => x.Album_Name)
                               orderby Album.MonthHits descending
                               select new HitViewModel
                               {
                                   Name = Album.Album_Name,
                                   MonthHits = Album.MonthHits
                               };
            return albumContext.Take(10).ToList();
        }



        public List<DetailViewModel> GetThisMonth()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "DECLARE @start AS  DATETIME " +
                             "SET @start = DATEADD(m, DATEDIFF(m, 0, GETDATE()), 0) " +
                             "DECLARE @end AS DATETIME " +
                             "SET @end = DATEADD(DAY, -1, DATEADD(m, DATEDIFF(m, 0, GETDATE()) + 1, 0)) " +
                             "SELECT TOP 10 " +
                             "Album_Name, " +
                             "SUM(od.Count * od.Price) AS TotalAmount " +
                             "FROM Album a " +
                             "INNER JOIN OrderDetail od ON od.Album_id = a.Album_id " +
                             "INNER JOIN[dbo].[Order] o ON  od.Order_id = o.Order_id " +
                             "WHERE o.Datetime BETWEEN @start AND @end " +
                             "GROUP BY Album_Name " +
                             "ORDER BY TotalAmount DESC";

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