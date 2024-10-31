﻿// <auto-generated />
using System;
using AdventureStoreApp.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdventureStoreApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdventureStoreApp.src.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CountryRegion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("StateProvince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("rowguid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AddressID");

                    b.ToTable("Address", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("CompanyName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NameStyle")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SalesPerson")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Suffix")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Title")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("rowguid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerID");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasFilter("[EmailAddress] IS NOT NULL");

                    b.ToTable("Customer", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.CustomerAddress", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AddressID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int?>("AddressID1")
                        .HasColumnType("int");

                    b.Property<int>("AddressType")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerID1")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("rowguid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerID", "AddressID");

                    b.HasIndex("AddressID");

                    b.HasIndex("AddressID1");

                    b.HasIndex("CustomerID1");

                    b.ToTable("CustomerAddress", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("Color")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime?>("DiscontinuedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("money");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductCategoryID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductModelID")
                        .HasColumnType("int");

                    b.Property<string>("ProductNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime?>("SellEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SellStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Size")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<decimal>("StandardCost")
                        .HasColumnType("money");

                    b.Property<byte[]>("ThumbNailPhoto")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ThumbnailPhotoFileName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<Guid>("rowguid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductID");

                    b.HasIndex("ProductCategoryID");

                    b.HasIndex("ProductModelID");

                    b.ToTable("Product", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductCategoryID"));

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("ParentProductCategoryID")
                        .HasColumnType("int");

                    b.Property<Guid>("rowguid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("ProductCategoryID");

                    b.HasIndex("ParentProductCategoryID");

                    b.ToTable("ProductCategory", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductDescription", b =>
                {
                    b.Property<int>("ProductDescriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductDescriptionID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("rowguid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("ProductDescriptionID");

                    b.ToTable("ProductDescription", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductModel", b =>
                {
                    b.Property<int>("ProductModelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductModelID"));

                    b.Property<string>("CatalogDescription")
                        .HasColumnType("xml");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("rowguid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("ProductModelID");

                    b.ToTable("ProductModel", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductModelProductDescription", b =>
                {
                    b.Property<int>("ProductModelID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("ProductDescriptionID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Culture")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("rowguid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("ProductModelID", "ProductDescriptionID", "Culture");

                    b.HasIndex("ProductDescriptionID");

                    b.ToTable("ProductModelProductDescription", "SalesLT");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.SalesOrderDetail", b =>
                {
                    b.Property<int>("SalesOrderID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("SalesOrderDetailID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<short>("OrderQty")
                        .HasColumnType("smallint");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("UnitPriceDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0.0m);

                    b.Property<Guid>("rowguid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("SalesOrderID", "SalesOrderDetailID");

                    b.HasIndex("ProductID");

                    b.ToTable("SalesOrderDetail", "SalesLT", t =>
                        {
                            t.HasCheckConstraint("CK_SalesOrderDetail_OrderQty", "[OrderQty] > 0");

                            t.HasCheckConstraint("CK_SalesOrderDetail_UnitPrice", "[UnitPrice] >= 0.00");

                            t.HasCheckConstraint("CK_SalesOrderDetail_UnitPriceDiscount", "[UnitPriceDiscount] >= 0.00");
                        });
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.SalesOrderHeader", b =>
                {
                    b.Property<int>("SalesOrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesOrderID"));

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BillToAddressID")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardApprovalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Freight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0.00m);

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("OnlineOrderFlag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("PurchaseOrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("RevisionNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)0);

                    b.Property<DateTime?>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ShipToAddressID")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<decimal>("SubTotal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0.00m);

                    b.Property<decimal>("TaxAmt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0.00m);

                    b.Property<Guid>("rowguid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("SalesOrderID");

                    b.HasIndex("BillToAddressID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ShipToAddressID");

                    b.ToTable("SalesOrderHeader", "SalesLT", t =>
                        {
                            t.HasCheckConstraint("CK_SalesOrderHeader_DueDate", "[DueDate] >= [OrderDate]");

                            t.HasCheckConstraint("CK_SalesOrderHeader_Freight", "[Freight] >= 0.00");

                            t.HasCheckConstraint("CK_SalesOrderHeader_ShipDate", "([ShipDate] >= [OrderDate] OR [ShipDate] IS NULL)");

                            t.HasCheckConstraint("CK_SalesOrderHeader_Status", "([Status] >= 0 AND [Status] <= 8)");

                            t.HasCheckConstraint("CK_SalesOrderHeader_SubTotal", "[SubTotal] >= 0.00");

                            t.HasCheckConstraint("CK_SalesOrderHeader_TaxAmt", "[TaxAmt] >= 0.00");
                        });
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.CustomerAddress", b =>
                {
                    b.HasOne("AdventureStoreApp.src.Models.Address", null)
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdventureStoreApp.src.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID1");

                    b.HasOne("AdventureStoreApp.src.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdventureStoreApp.src.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID1");

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.Product", b =>
                {
                    b.HasOne("AdventureStoreApp.src.Models.ProductCategory", null)
                        .WithMany()
                        .HasForeignKey("ProductCategoryID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AdventureStoreApp.src.Models.ProductModel", null)
                        .WithMany()
                        .HasForeignKey("ProductModelID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductCategory", b =>
                {
                    b.HasOne("AdventureStoreApp.src.Models.ProductCategory", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentProductCategoryID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductModelProductDescription", b =>
                {
                    b.HasOne("AdventureStoreApp.src.Models.ProductDescription", null)
                        .WithMany()
                        .HasForeignKey("ProductDescriptionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdventureStoreApp.src.Models.ProductModel", null)
                        .WithMany()
                        .HasForeignKey("ProductModelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.SalesOrderDetail", b =>
                {
                    b.HasOne("AdventureStoreApp.src.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdventureStoreApp.src.Models.SalesOrderHeader", null)
                        .WithMany()
                        .HasForeignKey("SalesOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.SalesOrderHeader", b =>
                {
                    b.HasOne("AdventureStoreApp.src.Models.Address", null)
                        .WithMany()
                        .HasForeignKey("BillToAddressID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AdventureStoreApp.src.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdventureStoreApp.src.Models.Address", null)
                        .WithMany()
                        .HasForeignKey("ShipToAddressID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AdventureStoreApp.src.Models.ProductCategory", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
