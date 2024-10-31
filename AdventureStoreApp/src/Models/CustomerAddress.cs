using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    public class CustomerAddress
    {
        [Key, Column(Order = 0)]
        public int CustomerID { get; set; }

        [Key, Column(Order = 1)]
        public int AddressID { get; set; }

        [Required]
        public AddressType AddressType { get; set; }

        [Required]
        public Guid rowguid { get; set; } // Use Guid for uniqueidentifier

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow; // Default value of getdate()

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
    }

    public enum AddressType
    {
        Home,
        Work,
        Other
    }
    // public class Name
    // {
    //     public string Value { get; set; }
    // }
}
