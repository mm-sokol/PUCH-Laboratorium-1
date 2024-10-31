using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    [Table("ProductModelProductDescription", Schema = "SalesLT")]
    public class ProductModelProductDescription
    {
        [Key, Column(Order = 0)]
        public int ProductModelID { get; set; }

        [Key, Column(Order = 1)]
        public int ProductDescriptionID { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        [StringLength(6)]
        public string Culture { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        public ProductModelProductDescription()
        {
            ModifiedDate = DateTime.Now; // Ustawienie domyślnej wartości
            rowguid = Guid.NewGuid(); // Ustawienie domyślnej wartości
        }
    }
}
