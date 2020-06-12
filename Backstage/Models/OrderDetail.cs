namespace Backstage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderDetail_id { get; set; }

        public int Order_id { get; set; }

        public int Album_id { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public virtual Album Album { get; set; }

        public virtual Order Order { get; set; }
    }
}
