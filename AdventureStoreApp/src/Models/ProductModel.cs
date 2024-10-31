using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    [Table("ProductModel", Schema = "SalesLT")]
    public class ProductModel
    {
        [Key]
        public int ProductModelID { get; set; }

        [Required]
        public string Name { get; set; } // Assuming 'Name' is a string type

        public string CatalogDescription { get; set; } // XML can be stored as string

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        public ProductModel()
        {
            ModifiedDate = DateTime.Now; // Ustawienie domyślnej wartości
            rowguid = Guid.NewGuid(); // Ustawienie domyślnej wartości
        }
    }
}
