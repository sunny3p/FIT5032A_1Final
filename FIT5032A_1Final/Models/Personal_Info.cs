namespace FIT5032A_1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Personal_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personal_Info()
        {
            Health_Info = new HashSet<Health_Info>();
            Locations = new HashSet<Location>();
            Reservations = new HashSet<Reservation>();
        }

        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please enter a valid First name Spaces and Numbers are not allowed")]
        public string Fname { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please enter a valid Last name Spaces and Numbers are not allowed")]
        public string Lname { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; }

        [Required]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Please enter a valid Phone no")]
        [StringLength(15)]
        public string Contact_No { get; set; }

        [Required]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Health_Info> Health_Info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location> Locations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
