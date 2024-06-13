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
Ten projekt jest licencjonowany na licencji MIT - zobacz plik [LICENSE](LICENSE) po więcej informacji.

## Autor
Marcin Krutak

## Przykład użycia

### Dodawanie sprzętu
```plaintext
> Wybierz opcję Rejestracja i utwórz konto administratora podając 6-cyfrowe ID (Twój login jest to kolejno pierwsze 6 cyfr z numeru PESEL RRMMDD)
> Podaj Imię i Nazwisko
> Utwórz swoje własne unikatowe hasło logowania.
> Wybierz opcję Logowanie i zaloguj się na konto abyś mógł dokonywać zmian pracując z programem.



