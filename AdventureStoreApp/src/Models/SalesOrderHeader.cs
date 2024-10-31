using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureStoreApp.src.Models
{
    [Table("SalesOrderHeader", Schema = "SalesLT")]
    public class SalesOrderHeader
    {
        [Key]
        public int SalesOrderID { get; set; }

        [Required]
        public byte RevisionNumber { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public bool OnlineOrderFlag { get; set; }

        [NotMapped] // To jest kolumna obliczeniowa w bazie danych
        public string SalesOrderNumber => $"SO{SalesOrderID}";

        public string PurchaseOrderNumber { get; set; }

        public string AccountNumber { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public int? ShipToAddressID { get; set; }

        public int? BillToAddressID { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipMethod { get; set; }

        public string CreditCardApprovalCode { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TaxAmt { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Freight { get; set; }

        [NotMapped] // To jest kolumna obliczeniowa w bazie danych
        public decimal TotalDue => SubTotal + TaxAmt + Freight;

        public string Comment { get; set; }

        [Required]
        public Guid rowguid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        public SalesOrderHeader()
        {
            ModifiedDate = DateTime.Now; // Ustawienie domyślnej wartości
            rowguid = Guid.NewGuid(); // Ustawienie domyślnej wartości
            OrderDate = DateTime.Now; // Ustawienie domyślnej wartości
            RevisionNumber = 0; // Ustawienie domyślnej wartości
            Status = 1; // Ustawienie domyślnej wartości
            OnlineOrderFlag = true; // Ustawienie domyślnej wartości
            SubTotal = 0.00m; // Ustawienie domyślnej wartości
            TaxAmt = 0.00m; // Ustawienie domyślnej wartości
            Freight = 0.00m; // Ustawienie domyślnej wartości
        }
    }
}
