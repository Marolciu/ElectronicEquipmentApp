using System;
using System.Collections.Generic;

namespace ElectronicEquipmentApp
{
    class Program
    {
        static string equipmentFilePath = "equipment.txt";
        static string employeeFilePath = "employees.txt";
        static EquipmentManager manager = new EquipmentManager();
        static LoginAndPersonManager adminManager = new LoginAndPersonManager();
        static bool hasSavedChanges = false;

        static void Main(string[] args)
        {
            Persons currentPerson = null;
            while (currentPerson == null)
            {
                Console.WriteLine("Witaj w ElectronicEquipmentApp:");
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Rejestracja jako Admin");
                Console.WriteLine("2. Rejestracja jako Użytkownik");
                Console.WriteLine("3. Logowanie");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 1:
                        adminManager.Register(true);
                        break;
                    case 2:
                        adminManager.Register(false);
                        break;
                    case 3:
                        currentPerson = adminManager.Login();
                        if (currentPerson == null)
                        {
                            Console.WriteLine("Logowanie nieudane, spróbuj ponownie.");
                        }
                        break;
                }
            }

            Console.WriteLine($"Zalogowano jako {currentPerson.Name} ({(currentPerson.IsAdmin ? "Administrator" : "Użytkownik")})");

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnConsoleCancelEvent);

            if (currentPerson.IsAdmin)
            {
                AdminMenu();
            }
            else
            {
                UserMenu();
            }
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            if (!hasSavedChanges)
            {
                AskToSaveChanges();
            }
        }

        static void OnConsoleCancelEvent(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            if (!hasSavedChanges)
            {
                AskToSaveChanges();
            }
        }

        static void AskToSaveChanges()
        {
            Console.WriteLine("Czy chcesz zapisać zmiany przed zamknięciem? (t/n)");
            string response = Console.ReadLine().ToLower();
            if (response == "t")
            {
                manager.WriteEquipmentToFile(equipmentFilePath, manager.EquipmentList);
                manager.WriteEmployeesToFile(employeeFilePath, manager.EmployeeList);
                Console.WriteLine("Dane zostały zapisane.");
            }
            else
            {
                Console.WriteLine("Zmiany nie zostały zapisane.");
            }
            hasSavedChanges = true;
            Environment.Exit(0);
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Sprzęt");
                Console.WriteLine("2. Pracownik");
                Console.WriteLine("3. Wyszukiwanie");
                Console.WriteLine("0. Wyjście");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 0:
                        AskToSaveChanges();
                        return;
                    case 1:
                        EquipmentMenu();
                        break;
                    case 2:
                        EmployeeMenu();
                        break;
                    case 3:
                        SearchMenu();
                        break;
                }
            }
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Wyszukiwanie");
                Console.WriteLine("0. Wyjście");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 1)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 0:
                        AskToSaveChanges();
                        return;
                    case 1:
                        SearchMenu();
                        break;
                }
            }
        }

        static void EquipmentMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj nowy sprzęt");
                Console.WriteLine("2. Edytuj istniejący sprzęt");
                Console.WriteLine("3. Przypisz sprzęt do pracownika lub zmień pracownika");
                Console.WriteLine("4. Usuń sprzęt");
                Console.WriteLine("0. Powrót do głównego menu");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 4)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        manager.AddEquipment();
                        hasSavedChanges = false;
                        break;
                    case 2:
                        manager.EditEquipment();
                        hasSavedChanges = false;
                        break;
                    case 3:
                        manager.AssignEquipment();
                        hasSavedChanges = false;
                        break;
                    case 4:
                        manager.DeleteEquipment();
                        hasSavedChanges = false;
                        break;
                }
            }
        }

        static void EmployeeMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj nowego pracownika");
                Console.WriteLine("2. Edytuj pracownika");
                Console.WriteLine("3. Usuń pracownika (Możliwe tylko gdy do pracownika nie należy już żaden sprzęt)");
                Console.WriteLine("0. Powrót do głównego menu");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        manager.AddEmployee();
                        hasSavedChanges = false;
                        break;
                    case 2:
                        EditEmployee();
                        hasSavedChanges = false;
                        break;
                    case 3:
                        DeleteEmployee();
                        hasSavedChanges = false;
                        break;
                }
            }
        }

        static void SearchMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Szukaj sprzętu");
                Console.WriteLine("2. Pokaż sprzęt przypisany do pracownika");
                Console.WriteLine("0. Powrót do głównego menu");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        SearchEquipmentById();
                        break;
                    case 2:
                        SearchEquipmentByEmployeeId();
                        break;
                }
            }
        }

        static void SearchEquipmentById()
        {
            try
            {
                Console.WriteLine("Wybierz opcję szukania:");
                Console.WriteLine("1. Szukaj po pełnym ID sprzętu");
                Console.WriteLine("2. Szukaj po części ID sprzętu");

                int searchChoice;
                while (!int.TryParse(Console.ReadLine(), out searchChoice) || searchChoice < 1 || searchChoice > 2)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (searchChoice)
                {
                    case 1:
                        Console.WriteLine("Podaj pełne ID sprzętu:");
                        string fullId = Console.ReadLine();
                        ElectronicEquipment equipmentById = manager.EquipmentList.FirstOrDefault(e => e.Id.ToString() == fullId);
                        if (equipmentById != null)
                        {
                            manager.DisplayEquipmentTable(new List<ElectronicEquipment> { equipmentById });
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono sprzętu o podanym ID.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Podaj część ID sprzętu:");
                        string partialId = Console.ReadLine();
                        List<ElectronicEquipment> equipmentByPartialId = manager.EquipmentList.Where(e => e.Id.ToString().Contains(partialId)).ToList();
                        if (equipmentByPartialId.Any())
                        {
                            manager.DisplayEquipmentTable(equipmentByPartialId);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono sprzętu o podanym ID.");
                        }
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wprowadzono nieprawidłowe dane. Spróbuj ponownie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }

        static void SearchEquipmentByEmployeeId()
        {
            try
            {
                Console.WriteLine("Wybierz opcję szukania:");
                Console.WriteLine("1. Szukaj po pełnym ID pracownika");
                Console.WriteLine("2. Szukaj po części ID pracownika");

                int searchChoice;
                while (!int.TryParse(Console.ReadLine(), out searchChoice) || searchChoice < 1 || searchChoice > 2)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (searchChoice)
                {
                    case 1:
                        Console.WriteLine("Podaj pełne ID pracownika:");
                        string fullEmployeeId = Console.ReadLine();
                        List<ElectronicEquipment> equipmentByFullEmployeeId = manager.EquipmentList.Where(e => e.AssignedEmployee != null && e.AssignedEmployee.Id.ToString() == fullEmployeeId).ToList();
                        if (equipmentByFullEmployeeId.Any())
                        {
                            manager.DisplayEquipmentTable(equipmentByFullEmployeeId);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono sprzętu przypisanego do pracownika o podanym ID.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Podaj część ID pracownika:");
                        string partialEmployeeId = Console.ReadLine();
                        List<ElectronicEquipment> equipmentByPartialEmployeeId = manager.EquipmentList.Where(e => e.AssignedEmployee != null && e.AssignedEmployee.Id.ToString().Contains(partialEmployeeId)).ToList();
                        if (equipmentByPartialEmployeeId.Any())
                        {
                            manager.DisplayEquipmentTable(equipmentByPartialEmployeeId);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono sprzętu przypisanego do pracownika o podanym ID.");
                        }
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wprowadzono nieprawidłowe dane. Spróbuj ponownie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }

        static void EditEmployee()
        {
            try
            {
                Console.WriteLine("Podaj ID pracownika do edycji:");
                int employeeId = int.Parse(Console.ReadLine());

                Employee employee = manager.EmployeeList.FirstOrDefault(u => u.Id == employeeId);
                if (employee != null)
                {
                    Console.WriteLine("Podaj nowe imię i nazwisko lub naciśnij Enter aby pozostawić niezmienione:");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        employee.Name = newName;
                    }

                    Console.WriteLine("Podaj nowy numer pokoju lub naciśnij Enter aby pozostawić niezmieniony:");
                    string newRoomNumber = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newRoomNumber))
                    {
                        employee.RoomNumber = newRoomNumber;
                    }

                    manager.WriteEmployeesToFile(employeeFilePath, manager.EmployeeList);
                    Console.WriteLine("Pracownik został zaktualizowany.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono pracownika o podanym ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wprowadzono nieprawidłowe dane. Spróbuj ponownie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }

        static void DeleteEmployee()
        {
            try
            {
                Console.WriteLine("Podaj ID pracownika do usunięcia:");
                int employeeId = int.Parse(Console.ReadLine());

                Employee employee = manager.EmployeeList.FirstOrDefault(u => u.Id == employeeId);
                if (employee != null)
                {
                    manager.EmployeeList.Remove(employee);
                    manager.WriteEmployeesToFile(employeeFilePath, manager.EmployeeList);
                    Console.WriteLine("Pracownik został usunięty.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono pracownika o podanym ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wprowadzono nieprawidłowe dane. Spróbuj ponownie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}
