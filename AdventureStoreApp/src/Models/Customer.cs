using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AdventureStoreApp.src.Models
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
        public string EmailAddress { get; set; }

        public string Phone { get; set; }

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


        public string Name { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
    }

    public enum NameStyle
    {
        Individual = 0,
        Company = 1
    }

    public class Phone
    {
        public string Number { get; set; }
    }
}
