using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backstage.ViewModel
{
    public class AlbumViewModel
    {
        public string Singer { get; set; }

        public int Album_id { get; set; }

        public int Singer_id { get; set; }

        public DateTime Datetime { get; set; }

        public string Album_Name { get; set; }

        public string Pic { get; set; }

        public string About { get; set; }

        public string Company { get; set; }

        public decimal Price { get; set; }
        public int Hits { get; set; }

        public int WeekHits { get; set; }

        public int MonthHits { get; set; }
    }
}