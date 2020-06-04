namespace Backstage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int Order_id { get; set; }

        public int Album_id { get; set; }

        [Required]
        [StringLength(128)]
        public string User_id { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public DateTime Datetime { get; set; }

        [Required]
        [StringLength(50)]
        public string Fedex { get; set; }

        [Required]
        [StringLength(20)]
        public string Credit_card { get; set; }

        [StringLength(128)]
        public string AspNetUsers_Id { get; set; }

        public virtual Album Album { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
