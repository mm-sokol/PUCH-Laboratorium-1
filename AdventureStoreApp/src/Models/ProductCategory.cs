using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    [Table("ProductCategory", Schema = "SalesLT")]
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        // Nawigacja do parent category
        [ForeignKey(nameof(ParentProductCategoryID))]
        public virtual ProductCategory ParentCategory { get; set; }

        // Nawigacja do subcategory (jeśli istnieje)
        public virtual ICollection<ProductCategory> SubCategories { get; set; }

        public ProductCategory()
        {
            SubCategories = new HashSet<ProductCategory>();
            ModifiedDate = DateTime.Now; // Ustawienie domyślnej wartości
            rowguid = Guid.NewGuid(); // Ustawienie domyślnej wartości
        }
    }
}
