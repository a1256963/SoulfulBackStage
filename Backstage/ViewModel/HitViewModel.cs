using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backstage.ViewModel
{
    public class HitViewModel
    {
        public string Name { get; set; }
        public double Hits { get; set; }
        public double WeekHits { get; set; }
        public double MonthHits { get; set; }
    }
}