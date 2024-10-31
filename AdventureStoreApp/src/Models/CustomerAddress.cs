using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.Models
{
    public class CustomerAddress
    {
        [Key, Column(Order = 0)]
        public int CustomerID { get; set; }

        [Key, Column(Order = 1)]
        public int AddressID { get; set; }

        [Required]
        public Name AddressType { get; set; }

        [Required]
        public Guid rowguid { get; set; } // Use Guid for uniqueidentifier

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow; // Default value of getdate()

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
    }

    // Assuming Name is defined elsewhere in your project
    public class Name
    {
        // Define properties for Name if needed
        public string Value { get; set; }
    }
}
