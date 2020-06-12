using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Backstage.ViewModel;
using Dapper;

namespace Backstage.Repositories
{
    public class SingerRepository
    {
        static string connString = ConfigurationManager.ConnectionStrings["soulful2020"].ToString();
        public List<SingerViewModel> GetAllSinger()
        {
            List<SingerViewModel> sins;
            using (SqlConnection conn=new SqlConnection(connString))
            {
                string sql = "select * from singer";
                sins = conn.Query<SingerViewModel>(sql).ToList();
            }
            return sins;
        }
    }
}