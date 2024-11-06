# Laboratorium 1

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
Server=tcp:puch.database.windows.net,1433;Initial Catalog=puch_db;Persist Security Info=False;User ID=puchlab;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;s
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

![alt text](screens3/image.png)

![alt text](screens3/image-1.png)

![alt text](screens3/image-2.png)

Zezwolenie na ruch sieciowy do portów rdp i http
![alt text](screens3/image-3.png)

![alt text](screens3/image-4.png)

<div style="padding: 10px; border: 1px solid #f5c6cb; background-color: #f8d7da; color: #721c24;">
  <strong>Nastąpił problem z łączeniem się do maszyny wirtualnej</strong> </br>
  Ze względu na to, że powtarzał się problem z połączeniem się przez rdp z maszyną wirtualną została ona usunięta. Podjęta została kolejna próba utworzenia maszyny wirtualnej.
</div>



3. Utworzenenie maszyny wirtualnej z SQL Server 

- Wybór opcji wdrożenia 'Maszyny wirtualne SQL'
![text](screens4/vm2.jpg)

- Ustawienie odpowiedniego regionu oraz strefy dostępności
![text](screens4/vm3.jpg)

- Pozostawienie obrazu Free SQL Server Licnese
![text](screens4/vm4.jpg)

- Wybór najmniejszej dostępnej ilości pamięci
![text](screens4/vm5.jpg)

- Uzupełnienie danych konta administratora
![text](screens4/vm6.jpg)

- Ustawienie możliwości połączenia się z maszyną przez rdp i http
![text](screens4/vm7.jpg)

- Wybór typu dysku twardego maszyny wirtualnej
![text](screens4/vm8.jpg)

- Publiczny adres IP został pozostawiony pusty
![text](screens4/vm9.jpg)

- Włączono możliwość uwierzytelnienia SQL
![text](screens4/vm10.jpg)

- Podsumowanie kosztów
![text](screens4/vm12.jpg)

- Posumowanie parametrów maszyny wirtualnej
![text](screens4/vm13.jpg)
![text](screens4/vm14.jpg)

- Niepowodzenie przy wdrażaniu
![text](screens4/vm15.jpg)

- Szczegóły błędu
![text](screens4/vm15_error.jpg)

- Sprawdzenie statusu SQL Servera po połączeniu się z maszyną wirtualną przez rdp
![text](screens4/vm16.jpg)

- Próba włączenia SQL Servera
![text](screens4/vm17.jpg)

- Błąd przy próbie włączenia SQL Servera
![text](screens4/vm18.jpg)

<div style="padding: 10px; border: 1px solid #f5c6cb; background-color: #f8d7da; color: #721c24;">
  <strong>Błąd przy wdrożeniu SQL Servera</strong> </br>
  Ze względu na problem przy wdrożeniu SQL Server na maszynie wirtualnej proces tworzenia został wykonany ponownie z wykorzystaniem tutorialu.
</div>

[video](https://www.youtube.com/watch?v=iHdQmnCcaOg)

4. Utworzenie maszyny wirtualnej 

- Utworzenie maszyny tym razem ponownie przez wybór zasobu 'Maszyna wirtualna'
![alt text](screens6/image.png)

- Wybór grupy zasobów
![alt text](screens6/image-1.png)

- Wybór nazwy maszyny oraz regionu 
![alt text](screens6/image-2.png)

- Strefa dostępności pozostała na opcji 'Strefa 1'
- Jako obraz maszyny pozostawiono wybór 'SQL Server 2019 Developer'
![alt text](screens6/image-3.png)

- Pozostawiono domyślny rozmiar dysku
![alt text](screens6/image-4.png)

- Uzupełniono login i hasło administratora
- Pozostawiono możliwość połączenia się z vm przez port rdp
- Zmieniono ustawienia dysku na wartość 'SSD Standard'
![alt text](screens6/image-5.png)

- Zabroniono połączeń przychodzących zzewnątrz 
![alt text](screens6/image-7.png)

- Ustawiono opcję 'Usuwanie publicznego adresu IP i karty sieciowej po usunięciu maszyny wirtualnej'

- Opcje równoważenia obciązenia pozostały domyślne ('Brak')

- Ustawiono opcje automatycznego zamykania
![alt text](screens6/image-8.png)

- Ustawona została opcja 'Łączność SQL' -> 'Publiczne (Internet)
![alt text](screens6/image-9.png)

- Włączone zostało uwierzytelnianie SQL
![alt text](screens6/image-10.png)

- Skonfigurowano magazyn wybierając 'Zmień konfigurację'
![alt text](screens6/image-11.png)

- Zmieniono typ dysku na mniejszy (16 GiB)
![alt text](screens6/image-12.png)

- Zmniejszono wielkość dysku magazynu dziennika do 16 GiB
![alt text](screens6/image-13.png)

- Wystąpił błąd związany z lokalizacją wybranego regionu
![alt text](screens6/image-14.png)

- Wyłączono opcję automatycznego zamykania, aby uzyskać pomyślną konfigurację
![alt text](screens6/image-15.png)

- Podsumowanie kosztów
![alt text](screens6/image-16.png)

- Uzyskano błąd wdrożenia

![alt text](screens7/image.png)
![alt text](screens7/image-1.png)


## Krok 7: Storage Account
1. Utworzenie konta magazynu (Storage Account)
- Wyszukanie w portalu 'storage account'
- Wybranie 'Konta magazynu'
- Wybranie przycisku 'Utwórz'

![alt text](screens3/image-5.png)

2. Konfiguracja konta
[CLI](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-cli)
- Dodanie konta do grupy zasobów
- Nazwanie konta magazynu
- Wybór regionu: (Europe) Poland Central
- Wybranie parametrów usługi
  - sku (Stock Keeping Unit)
  ```
  az storage account create: 'LRS' is not a valid value for '--sku'. Allowed values: Standard_LRS, Standard_GRS, Standard_RAGRS, Standard_ZRS, Premium_LRS, Premium_ZRS, Standard_GZRS, Standard_RAGZRS 
  ```
  _LRS_ - Locally Redundant Storage
    - Replikuje dane synchronicznie trzy razy w tym samym centrum danych w regionie podstawowym.
    - Zapewnia najmniejszy koszt i najniższą trwałość w porównaniu do innych opcji.
    - Chroni dane przed awariami serwerów i dysków, ale nie przed katastrofami, które dotykają całe centrum danych (np. pożar, powódź)

  _ZRS_ - Zone-Redundant Storage
   - Replikuje dane synchronicznie w trzech strefach dostępności Azure w regionie podstawowym
   - Każda strefa dostępności to oddzielna lokalizacja fizyczna z niezależnym zasilaniem, chłodzeniem i siecią
   - Zapewnia wyższą trwałość niż LRS, ponieważ dane są chronione przed awarią pojedynczej strefy dostępności

  _GRS_ - Geo-Redundant Storage
   - Replikuje dane synchronicznie trzy razy w regionie podstawowym (używając LRS)
   - Następnie asynchronicznie kopiuje dane do regionu pomocniczego, oddalonego o setki kilometrów
   - W regionie pomocniczym dane są również replikowane synchronicznie trzy razy (używając LRS)
   - Zapewnia najwyższy poziom trwałości
      RA-GRS (Read-Access Geo-Redundant Storage)
      RA-GZRS (Read-Access Geo-Zone-Redundant Storage)

![alt text](screens3/image-8.png)

![alt text](screens3/image-9.png)

```
{
  "accessTier": "Hot",
  "accountMigrationInProgress": null,
  "allowBlobPublicAccess": false,
  "allowCrossTenantReplication": false,
  "allowSharedKeyAccess": null,
  "allowedCopyScope": null,
  "azureFilesIdentityBasedAuthentication": null,
  "blobRestoreStatus": null,
  "creationTime": "2024-11-01T01:36:51.250595+00:00",
  "customDomain": null,
  "defaultToOAuthAuthentication": null,
  "dnsEndpointType": null,
  "enableExtendedGroups": null,
  "enableHttpsTrafficOnly": true,
  "enableNfsV3": null,
  "encryption": {
    "encryptionIdentity": null,
    "keySource": "Microsoft.Storage",
    "keyVaultProperties": null,
    "requireInfrastructureEncryption": null,
    "services": {
      "blob": {
        "enabled": true,
        "keyType": "Account",
        "lastEnabledTime": "2024-11-01T01:36:51.578626+00:00"
      },
      "file": {
        "enabled": true,
        "keyType": "Account",
        "lastEnabledTime": "2024-11-01T01:36:51.578626+00:00"
      },
      "queue": null,
      "table": null
    }
  },
  "extendedLocation": null,
  "failoverInProgress": null,
  "geoReplicationStats": null,
  "id": "/subscriptions/a3c39300-5bda-4650-8d3f-a2dcb45581d6/resourceGroups/puch-storage/providers/Microsoft.Storage/storageAccounts/puchstorage",
  "identity": null,
  "immutableStorageWithVersioning": null,
  "isHnsEnabled": null,
  "isLocalUserEnabled": null,
  "isSftpEnabled": null,
  "isSftpEnabled": null,
  "isSkuConversionBlocked": null,
  "keyCreationTime": {
    "key1": "2024-11-01T01:36:51.297374+00:00",
    "key2": "2024-11-01T01:36:51.297374+00:00"
  },
  "keyPolicy": null,
  "kind": "StorageV2",
  "largeFileSharesState": null,
  "lastGeoFailoverTime": null,
  "location": "polandcentral",
  "minimumTlsVersion": "TLS1_2",
  "name": "puchstorage",
  "networkRuleSet": {
    "bypass": "AzureServices",
    "defaultAction": "Allow",
    "ipRules": [],
    "ipv6Rules": [],
    "resourceAccessRules": null,
    "virtualNetworkRules": []
  },
  "primaryEndpoints": {
    "blob": "https://puchstorage.blob.core.windows.net/",
    "dfs": "https://puchstorage.dfs.core.windows.net/",
    "file": "https://puchstorage.file.core.windows.net/",
    "internetEndpoints": null,
    "microsoftEndpoints": null,
    "queue": "https://puchstorage.queue.core.windows.net/",
    "table": "https://puchstorage.table.core.windows.net/",
    "web": "https://puchstorage.z36.web.core.windows.net/"
  },
  "primaryLocation": "polandcentral",
  "privateEndpointConnections": [],
  "provisioningState": "Succeeded",
  "publicNetworkAccess": null,
  "resourceGroup": "puch-storage",
  "routingPreference": null,
  "sasPolicy": null,
  "secondaryEndpoints": null,
  "secondaryLocation": null,
  "sku": {
    "name": "Standard_LRS",
    "tier": "Standard"
  },
  "statusOfPrimary": "available",
  "statusOfSecondary": null,
  "storageAccountSkuConversionStatus": null,
  "tags": {},
  "type": "Microsoft.Storage/storageAccounts"
}
```
3. Tworzenie tabeli 

![alt text](screens3/image-11.png)

![alt text](screens3/image-10.png)

4. Dodanie rozszerzenia storage-preview

![alt text](screens3/image-12.png)

![alt text](screens3/image-13.png)

5. Instalacja Storage Explorer

6. Dodanie danych

![alt text](screens3/image-14.png)

![alt text](screens3/image-15.png)

##### Pobieranie danych z C# lub inna technologia:
1. Stworzenie prostej aplikacji webowej w .NET 

![alt text](screens3/image-16.png)

2. Dodanie pakietu obsługującego zapytania do Azure Table Storage

![alt text](screens3/image-17.png)

3. Dodanie klasy reprezentującej dane w tabeli WeatherData
```.NET
using Azure;
using Azure.Data.Tables;

public class WeatherData : ITableEntity
{
    public string PartitionKey { get; set; }

    public string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }

    // ...
}
```
3. Połączenie z kontem Azure Storage

- odnalezienie klucza do konta Azure Storage

![alt text](screens3/image-18.png)

- zapisanie klucza Key1

![alt text](screens3/image-19.png)

- utworzenie wpisu w pliku ```appsettings.json```

```JSON
  "TableStorage": {
    "ConnectionString": "...",
    "TableName": "WeatherData"
  }
```

4. Obsługa operacji CRUD

- Utworzona została aplikacja webowa w technologii .NET
```
dotnet new webapp -n CrudApp
```
- Należy zaimportować pakiet Azure.Data.Tables
```
dotnet add package Azure.Data.Tables
```
- Kluczowym elementem połączenia jest wskazanie parametrów połączenia w pliku appsettings.json
```
  "AzureTableStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=puchstorage;AccountKey=<key>;EndpointSuffix=core.windows.net",
    "TableName": "WeatherData"
  },
```
Jest on skopiowany ze strony portal.azure po wybraniu 'Konta magazynu' z listy zasobów Azure. Należy odnaleźć 'Zabezpieczenia i sieć' -> Klucze dostępu.

![alt text](screens7/image-2.png)

Następnie skopiować 'Parametry dostępu'

![alt text](screens7/image-3.png)

- Aby połączyć się z magazynem danych w kodzie muszą zostać przekazane odpowiednie argumenty z appsettings.json do obiektu TableClient z pakietu Azure.Data.Tables.
```
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//...

builder.Services.AddSingleton<TableClient>(sp =>
{
    var connectionString = configuration["AzureTableStorage:ConnectionString"];
    var tableName = configuration["AzureTableStorage:TableName"];
    var serviceClient = new TableServiceClient(connectionString);
    var tableClient = serviceClient.GetTableClient(tableName);
    tableClient.CreateIfNotExists();
    return tableClient;
});
```
- Dodany zostaje model utworzonej tabeli. Klasa ta musi implementować interfejs ITableEntity oraz uwzględniać pola PartitionKey i RowKey.
- Po utworzeniu obiektu aplikacji dodawane są endpointy realizujące wymagane operacje
```
var app = builder.Build();
// ...
app.MapPost("api/weather/{partitionKey}/{rowKey}", async (string partitionKey, string rowKey, WeatherInsertData weather, TableClient tableClient) =>
{
    try
    {
        // ... utworzenie nowego obiektu (data) typu WeatherData 
        // na podstawie otrzymanych danych 

        await tableClient.AddEntityAsync(data); // zapisanie rekordu
        return Results.Created($"/api/weather/{data.PartitionKey}/{data.RowKey}", data);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error occurred while inserting data: {ex.Message}");
    }
})
.WithName("InsertWeatherData");
```
- W podobny sposób dodane są możliwości czytania danych z tabeli w postaci obiektu json (```app.MapGet("api/weather", ...)```), aktualizacji rekordów (```app.MapPut("api/weather/{partitionKey}/{rowKey}", ...)```) i usuwania rekordów (```app.MapDelete("/api/weather/{partitionKey}/{rowKey}",...)```).

- Działanie zostało przetestowane przez aplikację POSTMAN. Dziełanie wymagało ustawienia nagłówka 'Content-Type' na 'application/json' oraz odznaczenia opcji weryfikcaji SSL:
![alt text](screens7/image-9.png)

  - Create
![alt text](screens7/image-5.png) 

  - Read
![alt text](screens7/image-6.png) 

  - Update 
![alt text](screens7/image-7.png)

  - Delete
![alt text](screens7/image-8.png)


## Azure Cosmos DB
#### 1. Stworzenie konta Azure Codsmos DB
- wyszukanie opcji Cosmos DB w portalu
- wybranie opcji: 'Wypróbuj usługę Azure Cosmos DB bezpłatnie'
- otworzenie Cosmos DB w profilu Azure

![alt text](screens7/image-10.png)

- zabezpieczenie konta z użyciem aplikacji MS Authenticator
- przy tworzeniu darmowego konta próbnego nie pojawiła się możliwość wyboru regionu, utworzone konto ma region East US

#### 2. Tworzenie bazy danych i kontenera
- połączenie z kontenerem przez Eksplorator danych

- tworzenie i konfiguracja kontenera
<!-- ![alt text](screens7/image-11.png) -->

![alt text](screens7/image-14.png)

<!-- ![alt text](screens7/image-15.png) -->
![alt text](screens7/image-17.png)

<!-- ![alt text](screens7/image-16.png) -->
<!-- ![alt text](screens7/image-18.png) -->
<!-- ![alt text](screens7/image-20.png) -->
![alt text](screens7/image-19.png)

<!-- - ustawienie RU Limit
![alt text](screens7/image-12.png) > ![alt text](screens7/image-13.png)
-->


#### 3. Dodawanie i pobieranie danych

- dodanie dokumentu przez Azure Data Explorer

![alt text](screens7/image-21.png)

![alt text](screens7/image-22.png)

![alt text](screens7/image-24.png)

- wykonanie zapytania w języku zapytań Cosmos DB

![alt text](screens7/image-25.png)

![alt text](screens7/image-26.png)

#### 4. Skalowanie i monitorowanie

- badanie możliwości skalowania poziomego w Cosmos DB -> modyfikacja liczby jednostek RU/s

<!-- ![alt text](screens7/image-28.png) -->
![alt text](screens7/image-29.png)

<!-- ![alt text](screens7/image-27.png) -->
![alt text](screens7/image-30.png)

- wykorzystanie Azure Monitor do śledzenia wykorzystania bazy danych

![alt text](screens7/image-31.png)

![alt text](screens7/image-32.png)

#### 5. Integracja z aplikacją

- utworzenie prostej aplikacji C#

- implementacja operacji CRUD na danych w CosmosDB
