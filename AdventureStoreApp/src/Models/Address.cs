using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; }

        [Required]
        [MaxLength(60)]
        public string AddressLine1 { get; set; }

        [MaxLength(60)]
        public string AddressLine2 { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        public Name StateProvince { get; set; }

        [Required]
        public Name CountryRegion { get; set; }

        [Required]
        [MaxLength(15)]
        public string PostalCode { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }

    // Assuming Name is defined elsewhere in your project
    public class Name
    {
        // Define properties for Name if needed
        public string Value { get; set; }
    }
}
