# Laboratorium 1
[TOC]
## Krok 1: Utworzenie konta AZURE
![alt text](screens/image-2.png)

## Krok 2: Utworzenie instancji Azure SQL Database
#### a. Tworzenie zasobu
![alt text](<screens/Zrzut ekranu z 2024-10-30 01-46-05.png>)

![alt text](screens/image.png)

![alt text](screens/image-1.png)

![alt text](screens/image-3.png)

#### b. Konfiguracja projketu
- tworzenie **grupy zasobów**
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
  Ustawienie tożsamości zarządzanai przypisanej przez system mogłoby ułatwić dostęp aplikacji do innych zasobów Azure (ożsamość zarządzana przypisana przez system pozwoli Ci na bezpieczne uwierzytelnianie bez konieczności przechowywania poświadczeń w kodzie)
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
![alt text](screens1/image-48.png)

![alt text](screens1/image-49.png)


![alt text](screens1/image-50.png)

![alt text](screens1/image-51.png)

```
{
    "status": "Failed",
    "error": {
        "code": "RedundancyConfigurationNotAvailableInRegion",
        "message": "The storage account failed to create due to redundancy configuration - Sku: Standard_RAGRS, Kind: StorageV2 - not available in selected Region - polandcentral. Please make sure the redundancy configuration selected is available in your region. For more information about redundancy configurations, see https://docs.microsoft.com/en-us/azure/storage/common/storage-redundancy"
    }
}
```

## Krok 4: Połączenie z bazą danych
1. Połączenie przez SSMS

![ssms](screens2/Zrzut%20ekranu%202024-10-31%20133252.png)
![ssms_done](screens2/Zrzut%20ekranu%202024-10-31%20133521.png)

2. Połączenie przez Azure Data Studio

![azure ds](screens2/azure%20data%20studio.png)
![azure ds done](screens2/azure%20data%20studio%20done.png)
