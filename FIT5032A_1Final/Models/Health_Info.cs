namespace FIT5032A_1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Health_Info
    {
        public int Id { get; set; }

        [Required]
        public string Alchol_Consumption { get; set; }

        [Required]
        public string Smoking { get; set; }
        [Range(100,200)]
        [Column(TypeName = "numeric")]
        public decimal Height { get; set; }
        [Range(15, 200)]
        [Column(TypeName = "numeric")]
        public decimal Weight { get; set; }

        [Required]
        public string Mood_Level { get; set; }

        //[DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        //[Required]
        //[StringLength(128)]
        public string PId { get; set; }

        public virtual Personal_Info Personal_Info { get; set; }
    }
}
