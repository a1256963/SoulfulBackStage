namespace Backstage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [Key]
        public int Event_id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int Singer_id { get; set; }

        public DateTime Datetime { get; set; }

        [Required]
        [StringLength(64)]
        public string Pic { get; set; }

        [Required]
        [StringLength(50)]
        public string Adress { get; set; }

        [Required]
        public string About { get; set; }

        public virtual Singer Singer { get; set; }
    }
}
