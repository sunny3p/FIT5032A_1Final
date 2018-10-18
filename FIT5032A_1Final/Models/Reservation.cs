namespace FIT5032A_1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        [Key]
        public int R_Id { get; set; }

        public DateTime R_DateTime { get; set; }

        [Required]
        public string Reason { get; set; }

        //[StringLength(128)]
        public string PId { get; set; }

        [StringLength(20)]
        public string R_Status { get; set; }

        [StringLength(128)]
        public string EId { get; set; }

        public virtual Employee_Info Employee_Info { get; set; }

        public virtual Personal_Info Personal_Info { get; set; }
    }
}
