using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string ProductNumber { get; set; }

        [MaxLength(15)]
        public string Color { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal StandardCost { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal ListPrice { get; set; }

        [MaxLength(5)]
        public string Size { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Weight { get; set; } // Nullable since it can be null

        public int? ProductCategoryID { get; set; }

        public int? ProductModelID { get; set; }

        [Required]
        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public byte[] ThumbNailPhoto { get; set; } // Use byte array for varbinary(max)

        [MaxLength(50)]
        public string ThumbnailPhotoFileName { get; set; }

        [Required]
        public Guid rowguid { get; set; } // Use Guid for uniqueidentifier

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow; // Default value of getdate()
    }

    // // Assuming Name is defined elsewhere in your project
    // public class Name
    // {
    //     // Define properties for Name if needed
    //     public string Value { get; set; }
    // }
}
