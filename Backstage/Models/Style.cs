namespace Backstage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Style")]
    public partial class Style
    {
        [Key]
        public int Style_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Style_type { get; set; }

        public int Album_id { get; set; }

        public virtual Album Album { get; set; }
    }
}
