# Laboratorium 1
[TOC]
## Krok 1: Utworzenie konta AZURE
- Uruchomienie wersji próbnej platformy Azure
![alt text](screens/image-2.png)

## Krok 2: Utworzenie instancji Azure SQL Database
#### a. Tworzenie zasobu
![alt text](<screens/Zrzut ekranu z 2024-10-30 01-46-05.png>)

- Wyszukanie SQL Database
![alt text](screens/image.png)

![alt text](screens/image-1.png)

![alt text](screens/image-3.png)

#### b. Konfiguracja projketu
- tworzenie grupy zasobów
  kontenera grupującego zasoby odpowiadające jednemu środowisku

  ![alt text](screens1/image-17.png)

- nazwanie bazy danych

  ![alt text](screens1/image-18.png)

#### c. Konfiguracja serwera

- utworzenie serwera 

  ![alt text](screens/image-6.png)

- określenie metody uwierzytelniania
  SQL, Microsoft Entra lub obie możliwości
  
  ![alt text](screens1/image-19.png)

#### e. Wybór opcji cenowych i rozmiaru
- Konfiguracja bazy danych

  ![alt text](screens1/image-20.png)
  ![alt text](screens1/image-21.png)

  Opcje liczby rdzeni i maksymalnej pamięci zostały pozostawione jako defaultowe
  ![alt text](screens1/image-22.png)

  <!-- ![alt text](screens1/image-23.png) -->

#### f. Dodatkowe ustawienia
###### Ustawienia sieci
- Łączność sieciowa została ustwiona na: Publiczny punkt końcowy

  ![alt text](screens1/image-24.png)

- Reguły zapory sieciowej skonfigurowano tak, aby możliwy był dostęp z aktualnego IP

  ![alt text](screens1/image-25.png)

- Zasady połączenia pozostały domyślne

  ![alt text](screens1/image-26.png)

- Połączenia szyfrowane pozostały na domyślnej minimalnej wersji protokołu TLS 1.2

  ![alt text](screens1/image-27.png)
###### Zabezpieczenia

 ![alt text](screens1/image-28.png)

- Rejestr kryptograficzny

  ![alt text](screens1/image-30.png)

  ![alt text](screens1/image-29.png)

- Magazyn skrótów
  ![alt text](screens1/image-31.png)

  ![alt text](screens1/image-32.png)

- Tożsamości serwera
  Ustawienie tożsamości zarządzanai przypisanej przez system mogłoby ułatwić dostęp aplikacji do innych zasobów Azure (tożsamość zarządzana przypisana przez system pozwoli Ci na bezpieczne uwierzytelnianie bez konieczności przechowywania poświadczeń w kodzie)
  ![alt text](screens1/image-38.png)

  ![alt text](screens1/image-35.png)

  ![alt text](screens1/image-36.png)

- Always Encrypted
  ![alt text](screens1/image-39.png)

- Źródło danych
  ![alt text](screens1/image-40.png)

- Tagi
  ![alt text](screens1/image-41.png)

###### Podsumowanie
![alt text](screens1/image-42.png)
![alt text](screens1/image-43.png)
![alt text](screens1/image-44.png)
![alt text](screens1/image-45.png)
![alt text](screens1/image-46.png)
![alt text](screens1/image-47.png)

## Krok 3: Zatwierdzenie i wdrożenie

## Krok 4: Połączenie z bazą danych
1. Połączenie przez SSMS

![ssms](screens2/Zrzut%20ekranu%202024-10-31%20133252.png)
![ssms_done](screens2/Zrzut%20ekranu%202024-10-31%20133521.png)

2. Połączenie przez Azure Data Studio

![azure ds](screens2/azure%20data%20studio.png)
![azure ds done](screens2/azure%20data%20studio%20done.png)


## Krok 5: Tworzenie aplikacji
1. Instalacja .NET SDK
2. Utworzenie szkieletu aplikacji
```
dotnet new console -n SimpleApp
```
3. Dodanie EntityFrameworkCore i SqlSever
```
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
```
4. Połączenie z bazą danych
- Odnalezienie paramatrów połączenia

![parametry](screens2/parametry%20po%C5%82%C4%85cze%C5%84.png)
```
Server=tcp:puch.database.windows.net,1433;Initial Catalog=puch_db;Persist Security Info=False;User ID=puchlab;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;s
``` 
5. Stworzenie małej tabeli MyTable
```
CREATE TABLE [SalesLT].[MyTable] (
    [ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Color] [nvarchar](15) NULL,
	[StandardCost] [money] NOT NULL,
    [ModifiedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_MyTable_ProductID] PRIMARY KEY CLUSTERED 
    (
        [ProductID] ASC
    )
);
INSERT INTO [SalesLT].[MyTable] ([Color], [StandardCost], [ModifiedDate])
SELECT [Color], [StandardCost], [ModifiedDate]
FROM [SalesLT].[Product];
```
6. Stworzenie klasy odzwierciedlającej atrybuty tabeli
```
namespace SimpleApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Color { get; set; }
        public decimal StandardCost { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
```
7. Stworzenie kontekstu połączenia z bazą danych
```
using Microsoft.EntityFrameworkCore;
using SimpleApp.Models;

namespace SimpleApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("MyTable", "SalesLT");
        }
    }
}
```
8. Utworzenie programu, który drukuje zawartość tabeli do konsoli
Wymaga utworzenia obiektu kontekstu połączenia AppDbContext
```
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer("<parametry połączenia>");
```
oraz wykorzystania go do pobrania danych z tabeli do zmiennej. 
```
using (var context = new AppDbContext(optionsBuilder.Options))
{
  var products = await context.Products.ToListAsync();

  Console.WriteLine("Products:");
  Console.WriteLine("ID\tColor\tStandard Cost\tModified Date");
  foreach (var product in products)
  {
      Console.WriteLine($"{product.ProductID}\t{product.Color}\t{product.StandardCost:C}\t{product.ModifiedDate:yyyy-MM-dd}");
  }
}
```

![alt text](screens2/image.png)

## Krok 6: Konfiguracja maszyny wirtualnej
1. Utworzenie maszyny wirtualnej

![alt text](image.png)

![alt text](image-1.png)

![alt text](image-2.png)

Zezwolenie na ruch sieciowy do portów rdp i http
![alt text](image-3.png)

![alt text](image-4.png)

3. Instalacja SQL Servera
4. Dostosowanie ustawień WindowsDefender


