# Tytuł projektu: ElectronicEquipmentApp

## Opis implementacji
Program ElectronicEquipmentApp jest aplikacją konsolową do zarządzania elektronicznym sprzętem i personelem w organizacji. Użytkownicy mogą logować się jako administratorzy lub zwykli użytkownicy, co determinuje zakres dostępnych opcji. Program oferuje różne menu i funkcje w zależności od roli użytkownika, w tym rejestrację, logowanie, zarządzanie sprzętem i pracownikami oraz wyszukiwanie informacji. Administratorzy mają pełny dostęp do wszystkich funkcji, podczas gdy zwykli użytkownicy mogą jedynie przeglądać dane sprzętu i jego przypisania.

## Funkcje aplikacji
Rejestracja i logowanie:
Użytkownik może zarejestrować się jako administrator lub zwykły użytkownik.
Logowanie za pomocą ID i hasła, które jest hashowane dla bezpieczeństwa.

Zarządzanie pracownikami:
Dodawanie nowych pracowników z unikalnym ID, imieniem i nazwiskiem oraz numerem pokoju.
Edytowanie informacji o istniejących pracownikach, takich jak imię, nazwisko i numer pokoju.
Usuwanie pracowników z systemu (jeśli nie mają przypisanego sprzętu).

Zarządzanie sprzętem elektronicznym:
Dodawanie nowego sprzętu z unikalnym ID, nazwą i specyficznymi właściwościami (np. CPU i RAM dla komputerów, rozmiar dla monitorów).
Edytowanie informacji o istniejącym sprzęcie.
Usuwanie sprzętu z systemu.
Przypisywanie sprzętu do pracowników lub zmiana przypisania.
Przeglądanie i wyszukiwanie sprzętu:

Wyszukiwanie sprzętu po ID lub części ID.
Wyświetlanie listy sprzętu przypisanego do konkretnego pracownika.
Zapisywanie i odczytywanie danych z bazy danych:

Przechowywanie informacji o pracownikach i sprzęcie w bazie SQLite.
Automatyczne tworzenie struktury bazy danych przy pierwszym uruchomieniu aplikacji.
Zapis zmian w bazie danych, w tym dodawanie, edytowanie i usuwanie rekordów.

## Opis klas
Klasa Persons: Przechowują informacje o osobach, w tym identyfikator, imię i nazwisko, hasło oraz status administratora.
Podklasy, Admin, User, Employee.

Klasa ElectronicEquipment: Przechowuje każdy rodzaj sprzętu oraz jego specyficzne właściwości, np. CPU i RAM dla komputerów lub numer telefonu dla podklasy Phone.
Podklasy, Computer, Monitor, Printer, Phone.

Program.cs
Klasa Program jest główną klasą uruchamiającą aplikację "ElectronicEquipmentApp". Odpowiada za interakcję użytkownika z aplikacją, zarządzanie sesją użytkownika oraz wywoływanie odpowiednich funkcji w zależności od wyborów użytkownika.

LoginAndPersonManager.cs
Klasa LoginAndPersonManager zarządza rejestracją, logowaniem oraz operacjami na użytkownikach (osobach) w aplikacji. Obejmuje funkcje związane z tworzeniem i zarządzaniem kontami użytkowników oraz przechowywaniem tych danych w bazie danych SQLite.

EquipmentManager.cs
Klasa EquipmentManager jest odpowiedzialna za zarządzanie sprzętem elektronicznym oraz pracownikami w aplikacji. Obejmuje funkcje związane z dodawaniem, edytowaniem, usuwaniem i przypisywaniem sprzętu, a także zarządzaniem danymi pracowników. Klasa ta również obsługuje interakcję z bazą danych SQLite, przechowującą informacje o sprzęcie i pracownikach.

## Jak uruchomić

### Wymagania
- Visual Studio (wersja 2019 lub nowsza)
- .NET Framework (ponieważ program jest napisany w C#)

### Kroki do uruchomienia
1. Otwórz Visual Studio.
2. Wybierz opcję sklouj repozytorium.
3. Wklej poniższy link.
    ```sh
    git clone https://github.com/Marolciu/ElectronicEquipmentApp.git
    ```
4. Wybierz ścieżkę lokalną i przejdź dalej.
5. Zbuduj i uruchom projekt, klikając `Start` lub naciskając `F5`.

## Licencja
Ten projekt jest licencjonowany na licencji GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 - zobacz plik [LICENSE.txt](LICENSE.txt) po więcej informacji.

## Autor
Marcin Krutak

## Zastosowanie elementów programowania obiektowego:

Dziedziczenie
Dziedziczenie pozwala tworzyć nowe klasy na podstawie już istniejących, co umożliwia ponowne wykorzystanie kodu i tworzenie hierarchii klas.

Klasa Persons i jej podklasy:

Klasa Persons jest bazową klasą dla Admin i User.
Admin dziedziczy po Persons i automatycznie ustawia wartość IsAdmin na true.
User dziedziczy po Admin i zmienia wartość IsAdmin na false.

![Obraz1](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/img/Zrzut%20ekranu%202024-06-22%20192630.png?raw=true)

Klasa ElectronicEquipment i jej podklasy:

Klasa ElectronicEquipment jest bazową klasą dla różnych typów sprzętu: Computer, Monitor, Printer, Phone.
Każda z tych klas dodaje specyficzne właściwości, takie jak CPU i RAM dla Computer czy Size dla Monitor.

![Obraz2](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/img/Zrzut%20ekranu%202024-06-22%20192639.png?raw=true)

Polimorfizm w metodach operujących na sprzęcie:
EquipmentManager przechowuje listę obiektów typu ElectronicEquipment, ale może przechowywać obiekty Computer, Monitor, Printer, Phone.

Metoda DisplayEquipment w EquipmentManager używa polimorfizmu do wyświetlania różnych typów sprzętu.

![Obraz3](https://github.com/Marolciu/ElectronicEquipmentApp/assets/148622198/9b3d7aad-4b4e-4203-a70f-1836f0cf9911)

Hermetyzacja (Enkapsulacja)
Hermetyzacja polega na ukrywaniu szczegółów implementacji i kontrolowaniu dostępu do danych poprzez publiczne metody i właściwości.

Ukrywanie szczegółów implementacji:

Pola w klasach Persons i ElectronicEquipment są prywatne lub chronione, a dostęp do nich odbywa się przez publiczne właściwości.
Hermetyzacja w LoginAndPersonManager:

![Obraz4](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/img/Zrzut%20ekranu%202024-06-22%20192708.png?raw=true)

Metody takie jak HashPassword i ReadPassword są prywatne, co oznacza, że są używane tylko wewnątrz klasy LoginAndPersonManager i nie są dostępne z zewnątrz.

![Obraz5](https://github.com/Marolciu/ElectronicEquipmentApp/assets/148622198/61e1f5e8-956c-4aab-b7ed-4204e62f57c1)

Podsumowanie
Projekt wykorzystuje podstawowe koncepcje programowania obiektowego, takie jak dziedziczenie, polimorfizm i hermetyzacja, aby stworzyć modularny i rozszerzalny system zarządzania sprzętem i użytkownikami. Dziedziczenie pozwala na ponowne wykorzystanie kodu, polimorfizm umożliwia elastyczne operowanie na różnych typach obiektów, a hermetyzacja zapewnia bezpieczeństwo i kontrolę nad danymi.

## Przykład użycia programu:
1. Rejestracja jako administrator i logowanie.
![Screen1](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/screens/Zrzut%20ekranu%202024-06-22%20195935.png?raw=true)

2. Dodawanie urządzenia.
![Screen2](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/screens/Zrzut%20ekranu%202024-06-22%20200054.png?raw=true)

3. Dodawanie pracownika.
![Screen3](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/screens/Zrzut%20ekranu%202024-06-22%20200152.png?raw=true)

4. Przypisanie sprzętu do pracownika.
![Screen4](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/screens/Zrzut%20ekranu%202024-06-22%20200249.png?raw=true)

5. Wyszukiwanie.
![Screen5](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/screens/Zrzut%20ekranu%202024-06-22%20201419.png?raw=true)

6. Podgląd na menu Użytkownika programu i wyszukiwanie z jego poziomu.
![Screen6](https://github.com/Marolciu/ElectronicEquipmentApp/blob/master/screens/Zrzut%20ekranu%202024-06-22%20201537.png?raw=true)
