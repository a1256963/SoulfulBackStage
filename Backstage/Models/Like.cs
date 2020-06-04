namespace Backstage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Like")]
    public partial class Like
    {
        [Key]
        public int Like_id { get; set; }

        public int Song_id { get; set; }

        [Required]
        [StringLength(128)]
        public string User_id { get; set; }

        [StringLength(128)]
        public string AspNetUsers_Id { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Song Song { get; set; }
    }
}
