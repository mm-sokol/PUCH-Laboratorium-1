using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        public NameStyle NameStyle { get; set; }

        [MaxLength(8)]
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string Suffix { get; set; }

        [MaxLength(128)]
        public string CompanyName { get; set; }

        [MaxLength(256)]
        public string SalesPerson { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }

        public Phone Phone { get; set; }

        [Required]
        [MaxLength(128)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(10)]
        public string PasswordSalt { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }

    // Assuming NameStyle and Phone are defined elsewhere in your project
    public enum NameStyle
    {
        // Define the possible values for NameStyle
        Individual = 0,
        Company = 1
    }

    public class Phone
    {
        public string Number { get; set; }
    }
}
