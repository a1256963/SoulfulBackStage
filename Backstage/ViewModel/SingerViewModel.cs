using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace Backstage.ViewModel
{
    public class SingerViewModel
    {
        public int Singer_id { get; set; }

       
        public string Name { get; set; }

  
        public string Gender { get; set; }

        public string Country { get; set; }

        public int Language_id { get; set; }
    }
}