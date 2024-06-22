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
Dziedziczenie: Główną klasą dla której zastosowano dziedziczenie jest klasa ElectronicEquipment. Jest ona klasą bazową dla pozostałych takich jak Komputer, Monitor, Drukarka i Telefon. Podobny zabieg zastosowałem podczas tworzenia głównej klasy Person do której należą podklasy takie jak Admin i User są to klasy które odpowiadają za użytkowników programu Admin ma wszystkie uprawnienia dodawania, edycji, przypisywania sprzętu i usuwania rekordów, za to User może tylko wyszukiwać informacje o sprzęcie i pracownikach go użytkujących. Podklasa Employee określa pracownika któremu można przypisać sprzęt.

Polimorfizm: Polimorfizm został wykorzystany poprzez Przesłanianie metod i Dynamiczne wiązanie.

W przypadku przesyłania metod Metoda ToString() jest przesłonięta w kilku klasach, co jest przykładem polimorfizmu. Każda klasa implementuje własną wersję tej metody, co pozwala na różne reprezentacje obiektów podczas konwersji do łańcucha znaków.

W przypadku Dynamicznego wiązania występuje ono w klasach zarządzających, takich jak AdminManager i EquipmentManager, stosuje się listy oraz metody operujące na bazowych klasach (Person, ElectronicEquipment). Dzięki temu można przechowywać obiekty pochodnych klas i wywoływać na nich metody w sposób polimorficzny.

Przykład w AdminManager.cs:
Metody ReadPersonsFromFile i WritePersonsToFile działają na obiektach typu Person, ale mogą operować na instancjach klas pochodnych Admin i User.

Przykład w EquipmentManager.cs:
Metody ReadEquipmentFromFile i WriteEquipmentToFile działają na obiektach typu ElectronicEquipment, ale mogą operować na instancjach klas pochodnych Computer, Monitor, Printer, i Phone.

Hermetyzacja: Została wykorzystana w klasach takich jak Person, Admin Manager i Equipment Manager.

Person: W tej klasie pola takie jak Id, Name, PasswordHash, i IsAdmin są hermetyzowane poprzez właściwości public z akcesorami get i set, które pozwalają na kontrolowanie dostępu i manipulację wartościami z zewnątrz klasy. Konstruktor klasy inicjalizuje te właściwości, zapewniając bezpieczne ustawianie wartości przy tworzeniu obiektu.

AdminManager: W tej klasie użyto prywatnych pól, takich jak filePath i personList, do przechowywania ścieżki do pliku i listy osób, co zapobiega bezpośredniemu dostępowi i modyfikacji tych pól z zewnątrz klasy. Prywatne metody ReadPersonsFromFile i WritePersonsToFile służą do hermetyzacji logiki odczytu i zapisu danych, co zwiększa bezpieczeństwo danych i utrzymuje klarowność interfejsów publicznych klasy.

EquipmentManager: Hermetyzacja w tej klasie manifestuje się poprzez użycie prywatnego pola equipmentFilePath oraz prywatnego settera dla właściwości EquipmentList, co ogranicza możliwość modyfikacji listy sprzętów bezpośrednio z zewnątrz klasy. Metoda ReadEquipmentFromFile jest prywatna, chroniąc logikę wczytywania danych przed dostępem zewnętrznym.

### Obsługa programu
```plaintext
1. Wprowadzenie
Aplikacja "ElectronicEquipmentApp" pozwala na rejestrację i zarządzanie użytkownikami, zarządzanie sprzętem elektronicznym oraz przypisywanie sprzętu do pracowników. Aplikacja skierowana jest głównie do administratorów systemu oraz użytkowników z odpowiednimi uprawnieniami.

2. Logowanie i Rejestracja
Rejestracja: Aby zarejestrować nowego użytkownika lub administratora, uruchom aplikację i wybierz opcję 1 dla rejestracji jako admin lub 2 jako użytkownik. Następnie podaj wymagane dane, takie jak ID, imię i nazwisko oraz hasło.
Logowanie: Aby zalogować się do systemu, uruchom aplikację i wybierz opcję 3. Podaj swoje ID oraz hasło.
3. Menu Administratora
Po zalogowaniu jako administrator, masz dostęp do następujących opcji:

Sprzęt:
Dodaj nowy sprzęt: Wybierz typ sprzętu i podaj jego szczegółowe dane.
Edytuj istniejący sprzęt: Aktualizuj dane wybranego sprzętu.
Przypisz sprzęt do pracownika: Wybierz sprzęt i przypisz go do wybranego pracownika.
Usuń sprzęt: Usuń wybrany sprzęt z systemu.
Pracownik:
Dodaj nowego pracownika: Podaj dane pracownika, w tym numer pokoju.
Edytuj pracownika: Zaktualizuj dane istniejącego pracownika.
Usuń pracownika: Usuń pracownika, który nie ma przypisanego sprzętu.
Wyszukiwanie:
Szukaj sprzętu: Wyszukaj sprzęt po ID.
Pokaż sprzęt przypisany do pracownika: Wyświetl wszystki sprzęt przypisany do wybranego pracownika.
4. Menu Użytkownika
Jako zwykły użytkownik masz dostęp do ograniczonego menu:

Wyszukiwanie:
Szukaj sprzętu: Wyszukaj sprzęt po ID.
Pokaż sprzęt przypisany do pracownika: Wyświetl wszystki sprzęt przypisany do wybranego pracownika.
5. Zapisywanie Zmian
Nie zapomnij zapisać zmian przed wyjściem z aplikacji. Pojawiające się monity o zapisanie zmian pozwolą ci zabezpieczyć wprowadzone modyfikacje.

6. Zakończenie Pracy z Aplikacją
Aby zakończyć pracę z aplikacją, wybierz opcję 0. Wyjście w głównym menu. Przed zamknięciem aplikacji zostaniesz poproszony o zapisanie zmian.
