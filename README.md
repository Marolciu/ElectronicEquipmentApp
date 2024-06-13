# ElectronicEquipmentApp

## Opis programu
ElectronicEquipmentApp to aplikacja konsolowa napisana w języku C#, która służy do zarządzania sprzętem elektronicznym w firmie. Aplikacja pozwala na rejestrowanie i logowanie administratorów, dodawanie, usuwanie i edytowanie sprzętu takiego jak telefony, komputery, drukarki i monitory oraz przypisywanie go do użytkowników. Program ma za zadanie ułatwiać inwentaryzację i kontrolę nad tym, kto jest w posiadaniu konkretnego urządzenia. Aplikacje umożliwia również wyświetlanie wszystkiego co znajduje się na "stanie" osoby której wydano sprzęt firmowy w celu realizacji zadań związanych z pracą w organizacji. Informacje zapisywane są w plikach .txt Program będzie sukcesywnie rozbudowywany o nowe funkcje i właściwości.

## Funkcje aplikacji
- **Rejestracja i logowanie administratorów**: Administratorzy mogą rejestrować się i logować do systemu, aby zarządzać sprzętem i użytkownikami.
- **Zarządzanie sprzętem**:
  - Dodawanie nowego sprzętu (komputery, monitory, drukarki, telefony).
  - Edytowanie istniejącego sprzętu.
  - Przypisywanie sprzętu do użytkowników.
  - Usuwanie sprzętu.
- **Zarządzanie użytkownikami**:
  - Dodawanie nowych użytkowników.
  - Edytowanie danych użytkowników.
  - Usuwanie użytkowników (jeśli nie mają przypisanego sprzętu).
- **Wyszukiwanie sprzętu**:
  - Wyszukiwanie sprzętu po pełnym lub częściowym ID.
  - Wyświetlanie sprzętu przypisanego do użytkownika.

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
Ten projekt jest licencjonowany na licencji MIT - zobacz plik [LICENSE.txt](LICENSE.txt) po więcej informacji.

## Autor
Marcin Krutak

### Obsługa programu
```plaintext
1. Rejestracja administratora: Pozwala na utworzenie nowego konta administratora.
2. Logowanie administratora: Pozwala zalogować się na istniejące konto
Po zalogowaniu dostępne jest menu główne:
1. Zarządzanie sprzętem:
> Dodawanie nowego sprzętu.
> Edycja istniejącego sprzętu.
> Przypisywanie sprzętu do użytkowników.
> Usuwanie sprzętu.
2. Zarządzanie użytkownikami:
> Dodawanie nowych użytkowników.
> Edycja użytkowników.
> Usuwanie użytkowników.
3. Wyszukiwanie sprzętu:
> Wyszukiwanie sprzętu po ID.
> Wyświetlanie sprzętu przypisanego do użytkownika.
Wyjście: Zakończenie działania aplikacji.

## Przykład użycia

### Rejestracja i logowanie
```plaintext
> Wybierz opcję Rejestracja i utwórz konto administratora podając 6-cyfrowe ID (Twój login jest to kolejno pierwsze 6 cyfr z numeru PESEL RRMMDD)
> Podaj Imię i Nazwisko
> Utwórz swoje własne unikatowe hasło logowania.
> Wybierz opcję Logowanie i zaloguj się na konto abyś mógł dokonywać zmian pracując z programem.
> Wybierz opcję dodaj sprzęt i podążaj za instrukcjami.
> Analogicznie dodaj nowego użytkownika który będzie posługiwał się urządzeniem.
> Otwórz menu sprzęt i przypisz sprzęt do użytkownika.
> Teraz moższ skorzystać z menu i wyszukać sprzęt przypisany do użytkownika.
> Aby poprawnie zamknąć program i zapisać zmiany w menu głównym wybierz opcję Wyjście oraz potwierdź zapisanie zmian klikając t i zatwierdź enter.

