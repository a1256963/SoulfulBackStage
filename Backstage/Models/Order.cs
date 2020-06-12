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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        public int Order_id { get; set; }

        public DateTime Datetime { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNetUsers_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RecieverName { get; set; }

        public int RecieverPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string RecieverAdress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
