using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureStoreApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigratin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SalesLT");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "SalesLT",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryRegion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "SalesLT",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameStyle = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    SalesPerson = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentProductCategoryID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryID);
                    table.ForeignKey(
                        name: "FK_ProductCategory_ProductCategory_ParentProductCategoryID",
                        column: x => x.ParentProductCategoryID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDescription",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductDescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescription", x => x.ProductDescriptionID);
                });

            migrationBuilder.CreateTable(
                name: "ProductModel",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CatalogDescription = table.Column<string>(type: "xml", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModel", x => x.ProductModelID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                schema: "SalesLT",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CustomerID1 = table.Column<int>(type: "int", nullable: true),
                    AddressID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => new { x.CustomerID, x.AddressID });
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "SalesLT",
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Address_AddressID1",
                        column: x => x.AddressID1,
                        principalSchema: "SalesLT",
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "SalesLT",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_CustomerID1",
                        column: x => x.CustomerID1,
                        principalSchema: "SalesLT",
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderHeader",
                schema: "SalesLT",
                columns: table => new
                {
                    SalesOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    OnlineOrderFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ShipToAddressID = table.Column<int>(type: "int", nullable: true),
                    BillToAddressID = table.Column<int>(type: "int", nullable: true),
                    ShipMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreditCardApprovalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0.00m),
                    TaxAmt = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0.00m),
                    Freight = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0.00m),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderHeader", x => x.SalesOrderID);
                    table.CheckConstraint("CK_SalesOrderHeader_DueDate", "[DueDate] >= [OrderDate]");
                    table.CheckConstraint("CK_SalesOrderHeader_Freight", "[Freight] >= 0.00");
                    table.CheckConstraint("CK_SalesOrderHeader_ShipDate", "([ShipDate] >= [OrderDate] OR [ShipDate] IS NULL)");
                    table.CheckConstraint("CK_SalesOrderHeader_Status", "([Status] >= 0 AND [Status] <= 8)");
                    table.CheckConstraint("CK_SalesOrderHeader_SubTotal", "[SubTotal] >= 0.00");
                    table.CheckConstraint("CK_SalesOrderHeader_TaxAmt", "[TaxAmt] >= 0.00");
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_Address_BillToAddressID",
                        column: x => x.BillToAddressID,
                        principalSchema: "SalesLT",
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_Address_ShipToAddressID",
                        column: x => x.ShipToAddressID,
                        principalSchema: "SalesLT",
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "SalesLT",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    StandardCost = table.Column<decimal>(type: "money", nullable: false),
                    ListPrice = table.Column<decimal>(type: "money", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: true),
                    ProductModelID = table.Column<int>(type: "int", nullable: true),
                    SellStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscontinuedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThumbNailPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailPhotoFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductModelProductDescription",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false),
                    ProductDescriptionID = table.Column<int>(type: "int", nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelProductDescription", x => new { x.ProductModelID, x.ProductDescriptionID, x.Culture });
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescription_ProductDescription_ProductDescriptionID",
                        column: x => x.ProductDescriptionID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductDescription",
                        principalColumn: "ProductDescriptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescription_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDetail",
                schema: "SalesLT",
                columns: table => new
                {
                    SalesOrderID = table.Column<int>(type: "int", nullable: false),
                    SalesOrderDetailID = table.Column<int>(type: "int", nullable: false),
                    OrderQty = table.Column<short>(type: "smallint", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    UnitPriceDiscount = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0.0m),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDetail", x => new { x.SalesOrderID, x.SalesOrderDetailID });
                    table.CheckConstraint("CK_SalesOrderDetail_OrderQty", "[OrderQty] > 0");
                    table.CheckConstraint("CK_SalesOrderDetail_UnitPrice", "[UnitPrice] >= 0.00");
                    table.CheckConstraint("CK_SalesOrderDetail_UnitPriceDiscount", "[UnitPriceDiscount] >= 0.00");
                    table.ForeignKey(
                        name: "FK_SalesOrderDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "SalesLT",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderDetail_SalesOrderHeader_SalesOrderID",
                        column: x => x.SalesOrderID,
                        principalSchema: "SalesLT",
                        principalTable: "SalesOrderHeader",
                        principalColumn: "SalesOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_EmailAddress",
                schema: "SalesLT",
                table: "Customer",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_AddressID",
                schema: "SalesLT",
                table: "CustomerAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_AddressID1",
                schema: "SalesLT",
                table: "CustomerAddress",
                column: "AddressID1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerID1",
                schema: "SalesLT",
                table: "CustomerAddress",
                column: "CustomerID1");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryID",
                schema: "SalesLT",
                table: "Product",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelID",
                schema: "SalesLT",
                table: "Product",
                column: "ProductModelID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ParentProductCategoryID",
                schema: "SalesLT",
                table: "ProductCategory",
                column: "ParentProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescription_ProductDescriptionID",
                schema: "SalesLT",
                table: "ProductModelProductDescription",
                column: "ProductDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDetail_ProductID",
                schema: "SalesLT",
                table: "SalesOrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_BillToAddressID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "BillToAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_CustomerID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_ShipToAddressID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "ShipToAddressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddress",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductModelProductDescription",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "SalesOrderDetail",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductDescription",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "SalesOrderHeader",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductModel",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "SalesLT");
        }
    }
}
