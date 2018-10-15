namespace FIT5032A_1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        public int Id { get; set; }

        [Required]
        public string Loc_Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        //[Required]
        //[StringLength(128)]
        public string PId { get; set; }

        public virtual Personal_Info Personal_Info { get; set; }
    }
}
