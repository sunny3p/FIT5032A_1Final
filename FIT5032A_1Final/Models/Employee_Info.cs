namespace FIT5032A_1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee_Info()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string Id { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please enter a valid First name Spaces and Numbers are not allowed")]
        [Required]
        public string Fname { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please enter a valid First name Spaces and Numbers are not allowed")]
        public string Lname { get; set; }

        [Required]
        public string Role { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Please enter a valid Phone no")]
        [Required]
        [StringLength(15)]
        public string Contact_No { get; set; }

        [Required]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
