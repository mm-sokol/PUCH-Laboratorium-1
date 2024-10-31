using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Models
{
    [Table("ProductDescription", Schema = "SalesLT")]
    public class ProductDescription
    {
        [Key]
        public int ProductDescriptionID { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        public ProductDescription()
        {
            ModifiedDate = DateTime.Now; // Ustawienie domyślnej wartości
            rowguid = Guid.NewGuid(); // Ustawienie domyślnej wartości
        }
    }
}
