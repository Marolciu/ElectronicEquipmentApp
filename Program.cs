using System;
using System.Collections.Generic;

namespace ElectronicEquipmentApp
{
    class Program
    {
        static string equipmentFilePath = "equipment.txt";
        static string userFilePath = "users.txt";
        static EquipmentManager manager = new EquipmentManager();
        static AdminManager adminManager = new AdminManager();
        static bool hasSavedChanges = false;

        static void Main(string[] args)
        {
            Admin currentAdmin = null;
            while (currentAdmin == null)
            {
                Console.WriteLine("Witaj w ElectronicEquipmentApp:");
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Rejestracja");
                Console.WriteLine("2. Logowanie");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (choice)
                {
                    case 1:
                        adminManager.Register();
                        break;
                    case 2:
                        currentAdmin = adminManager.Login();
                        if (currentAdmin == null)
                        {
                            Console.WriteLine("Logowanie nieudane, spróbuj ponownie.");
                        }
                        break;
                }
            }

            Console.WriteLine($"Zalogowano jako {currentAdmin.AdminName} ({(currentAdmin.IsAdmin ? "Administrator" : "Użytkownik")})");

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnConsoleCancelEvent);

            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Sprzęt");
                Console.WriteLine("2. Użytkownik");
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
                        UserMenu();
                        break;
                    case 3:
                        SearchMenu();
                        break;
                }
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
                manager.WriteUsersToFile(userFilePath, manager.UserList);
                Console.WriteLine("Dane zostały zapisane.");
            }
            else
            {
                Console.WriteLine("Zmiany nie zostały zapisane.");
            }
            hasSavedChanges = true;
            Environment.Exit(0);
        }

        static void EquipmentMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj nowy sprzęt");
                Console.WriteLine("2. Edytuj istniejący sprzęt");
                Console.WriteLine("3. Przypisz sprzęt do użytkownika lub zmień użytkownika");
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

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj nowego użytkownika");
                Console.WriteLine("2. Edytuj użytkownika");
                Console.WriteLine("3. Usuń użytkownika (Możliwe tylko gdy do użytkownika nie należy już żaden sprzęt)");
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
                        manager.AddUser();
                        hasSavedChanges = false;
                        break;
                    case 2:
                        EditUser();
                        hasSavedChanges = false;
                        break;
                    case 3:
                        DeleteUser();
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
                Console.WriteLine("2. Pokaż sprzęt przypisany do użytkownika");
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
                        SearchEquipmentByUserId();
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

        static void SearchEquipmentByUserId()
        {
            try
            {
                Console.WriteLine("Wybierz opcję szukania:");
                Console.WriteLine("1. Szukaj po pełnym ID użytkownika");
                Console.WriteLine("2. Szukaj po części ID użytkownika");

                int searchChoice;
                while (!int.TryParse(Console.ReadLine(), out searchChoice) || searchChoice < 1 || searchChoice > 2)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                switch (searchChoice)
                {
                    case 1:
                        Console.WriteLine("Podaj pełne ID użytkownika:");
                        string fullUserId = Console.ReadLine();
                        List<ElectronicEquipment> equipmentByFullUserId = manager.EquipmentList.Where(e => e.AssignedUser != null && e.AssignedUser.UserId.ToString() == fullUserId).ToList();
                        if (equipmentByFullUserId.Any())
                        {
                            manager.DisplayEquipmentTable(equipmentByFullUserId);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono sprzętu przypisanego do użytkownika o podanym ID.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Podaj część ID użytkownika:");
                        string partialUserId = Console.ReadLine();
                        List<ElectronicEquipment> equipmentByPartialUserId = manager.EquipmentList.Where(e => e.AssignedUser != null && e.AssignedUser.UserId.ToString().Contains(partialUserId)).ToList();
                        if (equipmentByPartialUserId.Any())
                        {
                            manager.DisplayEquipmentTable(equipmentByPartialUserId);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono sprzętu przypisanego do użytkownika o podanym ID.");
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

        static void EditUser()
        {
            try
            {
                Console.WriteLine("Podaj ID użytkownika do edycji:");
                int userId = int.Parse(Console.ReadLine());

                User user = manager.UserList.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    Console.WriteLine("Podaj nowe imię i nazwisko lub naciśnij Enter aby pozostawić niezmienione:");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        user.UserName = newName;
                    }

                    Console.WriteLine("Podaj nowy numer pokoju lub naciśnij Enter aby pozostawić niezmieniony:");
                    string newRoomNumber = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newRoomNumber))
                    {
                        user.RoomNumber = newRoomNumber;
                    }

                    manager.WriteUsersToFile(userFilePath, manager.UserList);
                    Console.WriteLine("Użytkownik został zaktualizowany.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono użytkownika o podanym ID.");
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

        static void DeleteUser()
        {
            try
            {
                Console.WriteLine("Podaj ID użytkownika do usunięcia:");
                int userId = int.Parse(Console.ReadLine());

                User user = manager.UserList.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    manager.UserList.Remove(user);
                    manager.WriteUsersToFile(userFilePath, manager.UserList);
                    Console.WriteLine("Użytkownik został usunięty.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono użytkownika o podanym ID.");
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
