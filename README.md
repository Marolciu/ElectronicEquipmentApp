<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ElectronicEquipmentApp</title>
</head>
<body>
    <h1>Tytuł projektu: ElectronicEquipmentApp</h1>
    
    <h2>Opis implementacji</h2>
    <p>Program ElectronicEquipmentApp jest aplikacją konsolową do zarządzania elektronicznym sprzętem i personelem w organizacji. Użytkownicy mogą logować się jako administratorzy lub zwykli użytkownicy, co determinuje zakres dostępnych opcji. Program oferuje różne menu i funkcje w zależności od roli użytkownika, w tym rejestrację, logowanie, zarządzanie sprzętem i pracownikami oraz wyszukiwanie informacji. Administratorzy mają pełny dostęp do wszystkich funkcji, podczas gdy zwykli użytkownicy mogą jedynie przeglądać dane sprzętu i jego przypisania.</p>
    
    <h2>Funkcje aplikacji</h2>
    <ul>
        <li><strong>Rejestracja i logowanie:</strong></li>
        <ul>
            <li>Użytkownik może zarejestrować się jako administrator lub zwykły użytkownik.</li>
            <li>Logowanie za pomocą ID i hasła, które jest hashowane dla bezpieczeństwa.</li>
        </ul>
        <li><strong>Zarządzanie pracownikami:</strong></li>
        <ul>
            <li>Dodawanie nowych pracowników z unikalnym ID, imieniem i nazwiskiem oraz numerem pokoju.</li>
            <li>Edytowanie informacji o istniejących pracownikach, takich jak imię, nazwisko i numer pokoju.</li>
            <li>Usuwanie pracowników z systemu (jeśli nie mają przypisanego sprzętu).</li>
        </ul>
        <li><strong>Zarządzanie sprzętem elektronicznym:</strong></li>
        <ul>
            <li>Dodawanie nowego sprzętu z unikalnym ID, nazwą i specyficznymi właściwościami (np. CPU i RAM dla komputerów, rozmiar dla monitorów).</li>
            <li>Edytowanie informacji o istniejącym sprzęcie.</li>
            <li>Usuwanie sprzętu z systemu.</li>
            <li>Przypisywanie sprzętu do pracowników lub zmiana przypisania.</li>
        </ul>
        <li><strong>Przeglądanie i wyszukiwanie sprzętu:</strong></li>
        <ul>
            <li>Wyszukiwanie sprzętu po ID lub części ID.</li>
            <li>Wyświetlanie listy sprzętu przypisanego do konkretnego pracownika.</li>
        </ul>
        <li><strong>Zapisywanie i odczytywanie danych z bazy danych:</strong></li>
        <ul>
            <li>Przechowywanie informacji o pracownikach i sprzęcie w bazie SQLite.</li>
            <li>Automatyczne tworzenie struktury bazy danych przy pierwszym uruchomieniu aplikacji.</li>
            <li>Zapis zmian w bazie danych, w tym dodawanie, edytowanie i usuwanie rekordów.</li>
        </ul>
    </ul>
    
    <h2>Opis klas</h2>
    <ul>
        <li><strong>Klasa Persons:</strong> Przechowują informacje o osobach, w tym identyfikator, imię i nazwisko, hasło oraz status administratora. Podklasy: Admin, User, Employee.</li>
        <li><strong>Klasa ElectronicEquipment:</strong> Bazowa klasa dla różnych typów sprzętu: Computer, Monitor, Printer, Phone. Każda z tych klas dodaje specyficzne właściwości, takie jak CPU i RAM dla Computer czy Size dla Monitor.</li>
    </ul>

    <h2>Instrukcje instalacji</h2>
    <ol>
        <li>Skopiuj repozytorium na swój lokalny system za pomocą komendy:
            <pre><code>git clone https://github.com/nazwa_uzytkownika/nazwa_repo.git</code></pre>
        </li>
        <li>Przejdź do katalogu projektu:
            <pre><code>cd nazwa_repo</code></pre>
        </li>
        <li>Uruchom aplikację, klikając "Start" lub naciskając "F5".</li>
    </ol>
    
    <h2>Licencja</h2>
    <p>Ten projekt jest licencjonowany na licencji GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 - zobacz plik <a href="LICENSE.txt">LICENSE.txt</a> po więcej informacji.</p>
    
    <h2>Autor</h2>
    <p>Marcin Krutak</p>
    
    <h2>Zastosowanie elementów programowania obiektowego</h2>
    <h3>Dziedziczenie</h3>
    <p>Dziedziczenie pozwala tworzyć nowe klasy na podstawie już istniejących, co umożliwia ponowne wykorzystanie kodu i tworzenie hierarchii klas.</p>
    <ul>
        <li><strong>Klasa Persons i jej podklasy:</strong></li>
        <ul>
            <li>Klasa Persons jest bazową klasą dla Admin i User.</li>
            <li>Admin dziedziczy po Persons i automatycznie ustawia wartość IsAdmin na true.</li>
            <li>User dziedziczy po Admin i zmienia wartość IsAdmin na false.</li>
        </ul>
        <li><strong>Klasa ElectronicEquipment i jej podklasy:</strong></li>
        <ul>
            <li>Klasa ElectronicEquipment jest bazową klasą dla różnych typów sprzętu: Computer, Monitor, Printer, Phone.</li>
            <li>Każda z tych klas dodaje specyficzne właściwości, takie jak CPU i RAM dla Computer czy Size dla Monitor.</li>
        </ul>
    </ul>
    
    <h3>Polimorfizm</h3>
    <p>Polimorfizm w metodach operujących na sprzęcie:</p>
    <p>Metoda DisplayEquipment w EquipmentManager używa polimorfizmu do wyświetlania różnych typów sprzętu.</p>
    
    <h3>Hermetyzacja (Enkapsulacja)</h3>
    <p>Hermetyzacja polega na ukrywaniu szczegółów implementacji i kontrolowaniu dostępu do danych poprzez publiczne metody i właściwości.</p>
    <ul>
        <li><strong>Ukrywanie szczegółów implementacji:</strong></li>
        <ul>
            <li>Pola w klasach Persons i ElectronicEquipment są prywatne lub chronione, a dostęp do nich odbywa się przez publiczne właściwości.</li>
        </ul>
        <li><strong>Hermetyzacja w LoginAndPersonManager:</strong></li>
        <ul>
            <li>Metody takie jak HashPassword i ReadPassword są prywatne, co oznacza, że są używane tylko wewnątrz klasy LoginAndPersonManager i nie są dostępne z zewnątrz.</li>
        </ul>
    </ul>
    
    <h2>Podsumowanie</h2>
    <p>Projekt wykorzystuje podstawowe koncepcje programowania obiektowego, takie jak dziedziczenie, polimorfizm i hermetyzacja, aby stworzyć modularny i rozszerzalny system zarządzania sprzętem i użytkownikami. Dziedziczenie pozwala na ponowne wykorzystanie kodu, polimorfizm umożliwia elastyczne operowanie na różnych typach obiektów, a hermetyzacja zapewnia bezpieczeństwo i kontrolę nad danymi.</p>
</body>
</html>
