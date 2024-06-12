# ElectronicEquipmentApp

## Opis
ElectronicEquipmentApp to prosta aplikacja konsolowa, która ułatwia zarządzanie sprzętem elektronicznym w organizacji. Pozwala na dodawanie sprzętu firmowego takiego jak telefony, komputery, drukarki i monitory oraz przypisywanie pracowników do używanego przez nich sprzętu elektronicznego, co ułatwia inwentaryzację i kontrolę nad tym, kto jest w posiadaniu konkretnego urządzenia. Aplikacje umożliwia również wyświetlanie wszystkiego co znajduje się na "stanie" osoby której wydano sprzęt firmowy w celu realizacji zadań związanych z pracą w organizacji. Aplikacja umożliwia tworzenie, edycję, usuwanie i wyszukiwanie sprzętu jak i danych powiązanego ze sprzętem pracownika. 

## Jak uruchomić

### Wymagania
- Visual Studio (wersja 2019 lub nowsza)
- .NET Framework (jeśli program jest napisany w C#)

### Kroki do uruchomienia
1. Skopiuj repozytorium:
    ```sh
    git clone https://github.com/Marolciu/ElectronicEquipmentApp.git
    ```
2. Otwórz Visual Studio.
3. W Visual Studio wybierz `File` -> `Open` -> `Project/Solution`.
4. Znajdź i otwórz plik `ElectronicEquipmentApp.sln`.
5. Zbuduj i uruchom projekt, klikając `Start` lub naciskając `F5`.

## Licencja
Ten projekt jest licencjonowany na licencji MIT - zobacz plik [LICENSE](LICENSE) po więcej informacji.

## Autor
Marcin Krutak

## Przykład użycia

### Dodawanie sprzętu
```plaintext
> Wpisz typ urządzenia (telefon, komputer, drukarka, monitor): komputer
> Podaj nazwę urządzenia: Dell XPS 15
> Podaj identyfikator pracownika: 123

