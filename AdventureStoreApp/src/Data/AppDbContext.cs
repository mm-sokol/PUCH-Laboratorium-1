using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdventureStoreApp.src.Models;

namespace AdventureStoreApp.src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelProductDescription> ProductModelProductDescroptions { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "SalesLT");
                
                entity.HasKey(c => c.CustomerID);
                
                entity.Property(c => c.CustomerID)
                    .ValueGeneratedOnAdd();
                
                entity.Property(c => c.Name)
                    .IsRequired();
                
                entity.Property(c => c.EmailAddress)
                    .HasMaxLength(50);

                entity.HasIndex(c => c.EmailAddress)
                    .IsUnique();
                
                entity.Property(c => c.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                // entity.OwnsOne(c => c.Phone);

            });

            // CustomerAddress
            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("CustomerAddress", "SalesLT");
                
                entity.HasKey(ca => new { ca.CustomerID, ca.AddressID });

                entity.Property(ca => ca.AddressType)
                    .IsRequired();

                entity.Property(ca => ca.ModifiedDate)
                    .HasDefaultValueSql("getdate()");
                
                // Relacje
                entity.HasOne<Customer>()
                    .WithMany(c => c.CustomerAddresses)
                    .HasForeignKey(ca => ca.CustomerID);

                entity.HasOne<Address>(ca => ca.Address)
                    .WithMany(a => a.CustomerAddresses)
                    .HasForeignKey(ca => ca.AddressID);
            });

            // Address
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "SalesLT");
                
                entity.HasKey(a => a.AddressID);
                
                entity.Property(a => a.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(30);
                
                entity.Property(a => a.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

            });

                modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "SalesLT");

                entity.HasKey(pc => pc.ProductCategoryID);

                entity.Property(pc => pc.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(pc => pc.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                entity.Property(pc => pc.rowguid)
                    .HasDefaultValueSql("newid()");

                // Relacje
                entity.HasOne(pc => pc.ParentCategory)
                    .WithMany(pc => pc.SubCategories)
                    .HasForeignKey(pc => pc.ParentProductCategoryID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ProductDescription
            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription", "SalesLT");

                entity.HasKey(pd => pd.ProductDescriptionID);

                entity.Property(pd => pd.Description)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(pd => pd.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                entity.Property(pd => pd.rowguid)
                    .HasDefaultValueSql("newid()");
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "SalesLT");
                
                entity.HasKey(p => p.ProductID);

                entity.Property(p => p.Name)
                    .IsRequired();

                entity.Property(p => p.ProductNumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(p => p.StandardCost)
                    .HasColumnType("money")
                    .IsRequired();

                entity.Property(p => p.ListPrice)
                    .HasColumnType("money")
                    .IsRequired();

                entity.Property(p => p.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                // Relacje
                entity.HasOne<ProductCategory>()
                    .WithMany()
                    .HasForeignKey(p => p.ProductCategoryID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<ProductModel>()
                    .WithMany()
                    .HasForeignKey(p => p.ProductModelID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("ProductModel", "SalesLT");

                entity.HasKey(pm => pm.ProductModelID);

                entity.Property(pm => pm.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(pm => pm.CatalogDescription)
                    .HasColumnType("xml");

                entity.Property(pm => pm.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                entity.Property(pm => pm.rowguid)
                    .HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ProductModelProductDescription>(entity =>
            {
                entity.ToTable("ProductModelProductDescription", "SalesLT");

                entity.HasKey(pm => new { pm.ProductModelID, pm.ProductDescriptionID, pm.Culture });

                entity.Property(pm => pm.Culture)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(pm => pm.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                entity.Property(pm => pm.rowguid)
                    .HasDefaultValueSql("newid()");

                // Relacje
                entity.HasOne<ProductModel>()
                    .WithMany()
                    .HasForeignKey(pm => pm.ProductModelID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<ProductDescription>()
                    .WithMany()
                    .HasForeignKey(pm => pm.ProductDescriptionID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.ToTable("SalesOrderDetail", "SalesLT", t=>{
                    t.HasCheckConstraint("CK_SalesOrderDetail_OrderQty", "[OrderQty] > 0");
                    t.HasCheckConstraint("CK_SalesOrderDetail_UnitPrice", "[UnitPrice] >= 0.00");
                    t.HasCheckConstraint("CK_SalesOrderDetail_UnitPriceDiscount", "[UnitPriceDiscount] >= 0.00");

                });

                entity.HasKey(sd => new { sd.SalesOrderID, sd.SalesOrderDetailID });

                entity.Property(sd => sd.OrderQty)
                    .IsRequired();
                

                entity.Property(sd => sd.UnitPrice)
                    .IsRequired()
                    .HasColumnType("money");


                entity.Property(sd => sd.UnitPriceDiscount)
                    .IsRequired()
                    .HasColumnType("money")
                    .HasDefaultValue(0.0m);


                entity.Property(sd => sd.rowguid)
                    .HasDefaultValueSql("newid()");

                entity.Property(sd => sd.ModifiedDate)
                    .HasDefaultValueSql("getdate()");

                // Relacje
                entity.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(sd => sd.ProductID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<SalesOrderHeader>()
                    .WithMany()
                    .HasForeignKey(sd => sd.SalesOrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.ToTable("SalesOrderHeader", "SalesLT", t=>{
                    // Ograniczenia
                    t.HasCheckConstraint("CK_SalesOrderHeader_DueDate", "[DueDate] >= [OrderDate]");
                    t.HasCheckConstraint("CK_SalesOrderHeader_Freight", "[Freight] >= 0.00");
                    t.HasCheckConstraint("CK_SalesOrderHeader_ShipDate", "([ShipDate] >= [OrderDate] OR [ShipDate] IS NULL)");
                    t.HasCheckConstraint("CK_SalesOrderHeader_Status", "([Status] >= 0 AND [Status] <= 8)");
                    t.HasCheckConstraint("CK_SalesOrderHeader_SubTotal", "[SubTotal] >= 0.00");
                    t.HasCheckConstraint("CK_SalesOrderHeader_TaxAmt", "[TaxAmt] >= 0.00");
                });

                entity.HasKey(so => so.SalesOrderID);

                entity.Property(so => so.RevisionNumber)
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.Property(so => so.OrderDate)
                    .IsRequired()
                    .HasDefaultValueSql("getdate()");

                entity.Property(so => so.Status)
                    .IsRequired()
                    .HasDefaultValue(1);

                entity.Property(so => so.OnlineOrderFlag)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(so => so.SubTotal)
                    .IsRequired()
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(so => so.TaxAmt)
                    .IsRequired()
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(so => so.Freight)
                    .IsRequired()
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(so => so.rowguid)
                    .IsRequired()
                    .HasDefaultValueSql("newid()");

                entity.Property(so => so.ModifiedDate)
                    .IsRequired()
                    .HasDefaultValueSql("getdate()");

                // Relacje
                entity.HasOne<Address>()
                    .WithMany()
                    .HasForeignKey(so => so.BillToAddressID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Address>()
                    .WithMany()
                    .HasForeignKey(so => so.ShipToAddressID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey(so => so.CustomerID)
                    .OnDelete(DeleteBehavior.Restrict);

            });
        }
    }
}
