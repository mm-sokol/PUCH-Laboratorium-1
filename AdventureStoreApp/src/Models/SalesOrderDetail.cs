using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    [Table("SalesOrderDetail", Schema = "SalesLT")]
    public class SalesOrderDetail
    {
        [Key, Column(Order = 0)]
        public int SalesOrderID { get; set; }

        [Key, Column(Order = 1)]
        public int SalesOrderDetailID { get; set; }

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Order quantity must be greater than 0.")]
        public short OrderQty { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPriceDiscount { get; set; }

        [NotMapped] // Używamy [NotMapped] ponieważ to jest kolumna obliczeniowa w bazie danych
        public decimal LineTotal => (UnitPrice * (1.0m - UnitPriceDiscount)) * OrderQty;

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        public SalesOrderDetail()
        {
            ModifiedDate = DateTime.Now; // Ustawienie domyślnej wartości
            rowguid = Guid.NewGuid(); // Ustawienie domyślnej wartości
        }
    }
}
