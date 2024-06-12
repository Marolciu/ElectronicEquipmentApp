using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace ElectronicEquipmentApp
{
    class EquipmentManager
    {
        private string userFilePath = "users.txt";
        private string equipmentFilePath = "equipment.txt";

        public List<User> UserList { get; private set; } = new List<User>();
        public List<ElectronicEquipment> EquipmentList { get; private set; } = new List<ElectronicEquipment>();

        public EquipmentManager()
        {
            UserList = ReadUsersFromFile();
            EquipmentList = ReadEquipmentFromFile(equipmentFilePath);
        }

        public void AddUser()
        {
            try
            {
                Console.WriteLine("Podaj ID użytkownika:");
                int userId = int.Parse(Console.ReadLine());

                if (UserList.Any(u => u.UserId == userId))
                {
                    Console.WriteLine("Użytkownik o podanym ID już istnieje.");
                    return;
                }

                Console.WriteLine("Podaj imię i nazwisko:");
                string userName = Console.ReadLine();

                Console.WriteLine("Podaj numer pokoju:");
                string roomNumber = Console.ReadLine();

                User user = new User(userId, userName, roomNumber);
                UserList.Add(user);
                WriteUsersToFile(userFilePath, UserList);
                Console.WriteLine("Użytkownik został dodany.");
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

        public void AssignEquipment()
        {
            try
            {
                Console.WriteLine("Podaj ID sprzętu:");
                int equipmentId = int.Parse(Console.ReadLine());

                ElectronicEquipment equipment = EquipmentList.FirstOrDefault(e => e.Id == equipmentId);
                if (equipment == null)
                {
                    Console.WriteLine("Nie znaleziono sprzętu o podanym ID.");
                    return;
                }

                Console.WriteLine("Podaj ID użytkownika:");
                int userId = int.Parse(Console.ReadLine());

                User user = UserList.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    Console.WriteLine("Nie znaleziono użytkownika o podanym ID.");
                    return;
                }

                equipment.AssignedUser = user;
                WriteEquipmentToFile(equipmentFilePath, EquipmentList);
                Console.WriteLine("Sprzęt został przypisany użytkownikowi.");
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

        public void AddEquipment()
        {
            try
            {
                Console.WriteLine("Wybierz typ sprzętu:");
                Console.WriteLine("1. Komputer");
                Console.WriteLine("2. Monitor");
                Console.WriteLine("3. Drukarka");
                Console.WriteLine("4. Telefon");

                int equipmentType;
                while (!int.TryParse(Console.ReadLine(), out equipmentType) || equipmentType < 1 || equipmentType > 4)
                {
                    Console.WriteLine("Niepoprawny wybór. Wybierz ponownie.");
                }

                Console.WriteLine("Podaj ID sprzętu:");
                int id = int.Parse(Console.ReadLine());

                if (EquipmentList.Any(e => e.Id == id))
                {
                    Console.WriteLine("Sprzęt o podanym ID już istnieje.");
                    return;
                }

                Console.WriteLine("Podaj nazwę sprzętu:");
                string name = Console.ReadLine();

                ElectronicEquipment equipment = null;

                switch (equipmentType)
                {
                    case 1:
                        Console.WriteLine("Podaj CPU:");
                        string cpu = Console.ReadLine();

                        Console.WriteLine("Podaj ilość RAM (GB):");
                        int ram = int.Parse(Console.ReadLine());

                        equipment = new Computer(id, name, cpu, ram);
                        break;
                    case 2:
                        Console.WriteLine("Podaj rozmiar monitora (cale):");
                        int size = int.Parse(Console.ReadLine());

                        equipment = new Monitor(id, name, size);
                        break;
                    case 3:
                        Console.WriteLine("Podaj typ drukarki (np. laserowa, atramentowa):");
                        string type = Console.ReadLine();

                        equipment = new Printer(id, name, type);
                        break;
                    case 4:
                        Console.WriteLine("Podaj numer telefonu:");
                        string phoneNumber = Console.ReadLine();

                        equipment = new Phone(id, name, phoneNumber);
                        break;
                }

                EquipmentList.Add(equipment);
                WriteEquipmentToFile(equipmentFilePath, EquipmentList);
                Console.WriteLine("Sprzęt został dodany.");
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

        public void EditEquipment()
        {
            try
            {
                Console.WriteLine("Podaj ID sprzętu do edycji:");
                int id = int.Parse(Console.ReadLine());

                ElectronicEquipment equipment = EquipmentList.FirstOrDefault(e => e.Id == id);
                if (equipment != null)
                {
                    Console.WriteLine("Podaj nową nazwę sprzętu lub naciśnij Enter aby pozostawić niezmienioną:");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        equipment.Name = newName;
                    }

                    if (equipment is Computer computer)
                    {
                        Console.WriteLine("Podaj nowe CPU lub naciśnij Enter aby pozostawić niezmienione:");
                        string newCPU = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCPU))
                        {
                            computer.CPU = newCPU;
                        }

                        Console.WriteLine("Podaj nową ilość RAM (GB) lub naciśnij Enter aby pozostawić niezmienioną:");
                        string newRAMInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newRAMInput))
                        {
                            int newRAM = int.Parse(newRAMInput);
                            computer.RAM = newRAM;
                        }
                    }
                    else if (equipment is Monitor monitor)
                    {
                        Console.WriteLine("Podaj nowy rozmiar monitora (cale) lub naciśnij Enter aby pozostawić niezmieniony:");
                        string newSizeInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newSizeInput))
                        {
                            int newSize = int.Parse(newSizeInput);
                            monitor.Size = newSize;
                        }
                    }
                    else if (equipment is Printer printer)
                    {
                        Console.WriteLine("Podaj nowy typ drukarki lub naciśnij Enter aby pozostawić niezmieniony:");
                        string newType = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newType))
                        {
                            printer.Type = newType;
                        }
                    }
                    else if (equipment is Phone phone)
                    {
                        Console.WriteLine("Podaj nowy numer telefonu lub naciśnij Enter aby pozostawić niezmieniony:");
                        string newPhoneNumber = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newPhoneNumber))
                        {
                            phone.PhoneNumber = newPhoneNumber;
                        }
                    }

                    WriteEquipmentToFile(equipmentFilePath, EquipmentList);
                    Console.WriteLine("Sprzęt został zaktualizowany.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono sprzętu o podanym ID.");
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

        public void DisplayEquipment(ElectronicEquipment equipment)
        {
            if (equipment != null)
            {
                Console.WriteLine($"ID: {equipment.Id}");
                Console.WriteLine($"Nazwa: {equipment.Name}");
                if (equipment.AssignedUser != null)
                {
                    Console.WriteLine($"Użytkownik: {equipment.AssignedUser.UserName}");
                }

                if (equipment is Computer computer)
                {
                    Console.WriteLine($"CPU: {computer.CPU}");
                    Console.WriteLine($"RAM: {computer.RAM} GB");
                }
                else if (equipment is Monitor monitor)
                {
                    Console.WriteLine($"Rozmiar: {monitor.Size} cale");
                }
                else if (equipment is Printer printer)
                {
                    Console.WriteLine($"Typ: {printer.Type}");
                }
                else if (equipment is Phone phone)
                {
                    Console.WriteLine($"Numer telefonu: {phone.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("Sprzęt nie został znaleziony.");
            }
        }

        public void DisplayEquipmentTable(List<ElectronicEquipment> equipmentList)
        {
            string header = "| {0,-6} | {1,-25} | {2,-9} | {3,-18} | {4,-12} | {5,-4} | {6,-7} | {7,-13} | {8,-14} |";
            string separator = new string('-', 136);

            Console.WriteLine(separator);
            Console.WriteLine(header, "ID", "Nazwa", "Typ", "Użytkownik", "CPU", "RAM", "Rozmiar", "Typ Drukarki", "Numer Telefonu");
            Console.WriteLine(separator);

            foreach (var equipment in equipmentList)
            {
                string equipmentType = equipment.GetType().Name;
                string userName = equipment.AssignedUser != null ? equipment.AssignedUser.UserName : "Brak";
                string cpu = equipment is Computer computer ? computer.CPU : "";
                string ram = equipment is Computer computer2 ? computer2.RAM.ToString() : "";
                string size = equipment is Monitor monitor ? monitor.Size.ToString() : "";
                string printerType = equipment is Printer printer ? printer.Type : "";
                string phoneNumber = equipment is Phone phone ? phone.PhoneNumber : "";

                Console.WriteLine(header, equipment.Id, equipment.Name, equipmentType, userName, cpu, ram, size, printerType, phoneNumber);
            }

            Console.WriteLine(separator);
        }

        public void DeleteEquipment()
        {
            try
            {
                Console.WriteLine("Podaj ID sprzętu do usunięcia:");
                int id = int.Parse(Console.ReadLine());

                ElectronicEquipment equipment = EquipmentList.FirstOrDefault(e => e.Id == id);
                if (equipment != null)
                {
                    EquipmentList.Remove(equipment);
                    WriteEquipmentToFile(equipmentFilePath, EquipmentList);
                    Console.WriteLine("Sprzęt został usunięty.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono sprzętu o podanym ID.");
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

        public List<ElectronicEquipment> ReadEquipmentFromFile(string filePath)
        {
            List<ElectronicEquipment> equipmentList = new List<ElectronicEquipment>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    try
                    {
                        if (parts.Length >= 5)
                        {
                            int id = int.Parse(parts[0]);
                            string type = parts[1];
                            string name = parts[2];
                            User user = null;

                            if (!string.IsNullOrWhiteSpace(parts[3]) && !string.IsNullOrWhiteSpace(parts[4]))
                            {
                                int userId = int.Parse(parts[3]);
                                string userName = parts[4];
                                user = UserList.FirstOrDefault(u => u.UserId == userId && u.UserName == userName);
                                if (user == null)
                                {
                                    user = new User(userId, userName, "Unknown");
                                    UserList.Add(user);
                                }
                            }

                            switch (type.ToLower())
                            {
                                case "komputer":
                                    if (parts.Length == 7)
                                    {
                                        string cpu = parts[5];
                                        int ram = int.Parse(parts[6]);
                                        equipmentList.Add(new Computer(id, name, cpu, ram, user));
                                    }
                                    break;
                                case "monitor":
                                    if (parts.Length == 6)
                                    {
                                        int size = int.Parse(parts[5]);
                                        equipmentList.Add(new Monitor(id, name, size, user));
                                    }
                                    break;
                                case "drukarka":
                                    if (parts.Length == 6)
                                    {
                                        string printerType = parts[5];
                                        equipmentList.Add(new Printer(id, name, printerType, user));
                                    }
                                    break;
                                case "telefon":
                                    if (parts.Length == 6)
                                    {
                                        string phoneNumber = parts[5];
                                        equipmentList.Add(new Phone(id, name, phoneNumber, user));
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"Nieoczekiwany typ sprzętu: {type}");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Nieoczekiwany format danych w linii: {line}");
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Błąd formatu w linii: {line}. Szczegóły: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas przetwarzania linii: {line}. Szczegóły: {ex.Message}");
                    }
                }
            }

            return equipmentList;
        }

        public List<User> ReadUsersFromFile()
        {
            List<User> userList = new List<User>();

            if (File.Exists(userFilePath))
            {
                string[] lines = File.ReadAllLines(userFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    try
                    {
                        if (parts.Length == 3)
                        {
                            int userId = int.Parse(parts[0]);
                            string userName = parts[1];
                            string roomNumber = parts[2];
                            userList.Add(new User(userId, userName, roomNumber));
                        }
                        else
                        {
                            Console.WriteLine($"Nieoczekiwany format danych w linii: {line}");
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Błąd formatu w linii: {line}. Szczegóły: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas przetwarzania linii: {line}. Szczegóły: {ex.Message}");
                    }
                }
            }

            return userList;
        }

        public void WriteEquipmentToFile(string filePath, List<ElectronicEquipment> equipmentList)
        {
            List<string> lines = new List<string>();
            foreach (var equipment in equipmentList)
            {
                if (equipment is Computer computer)
                {
                    string line = $"{computer.Id},Komputer,{computer.Name},{computer.AssignedUser?.UserId},{computer.AssignedUser?.UserName},{computer.CPU},{computer.RAM}";
                    lines.Add(line);
                }
                else if (equipment is Monitor monitor)
                {
                    string line = $"{monitor.Id},Monitor,{monitor.Name},{monitor.AssignedUser?.UserId},{monitor.AssignedUser?.UserName},{monitor.Size}";
                    lines.Add(line);
                }
                else if (equipment is Printer printer)
                {
                    string line = $"{printer.Id},Drukarka,{printer.Name},{printer.AssignedUser?.UserId},{printer.AssignedUser?.UserName},{printer.Type}";
                    lines.Add(line);
                }
                else if (equipment is Phone phone)
                {
                    string line = $"{phone.Id},Telefon,{phone.Name},{phone.AssignedUser?.UserId},{phone.AssignedUser?.UserName},{phone.PhoneNumber}";
                    lines.Add(line);
                }
                else
                {
                    string line = $"{equipment.Id},Nieznany,{equipment.Name},{equipment.AssignedUser?.UserId},{equipment.AssignedUser?.UserName}";
                    lines.Add(line);
                }
            }
            File.WriteAllLines(filePath, lines);
        }

        public void WriteUsersToFile(string filePath, List<User> userList)
        {
            List<string> lines = new List<string>();
            foreach (var user in userList)
            {
                string line = $"{user.UserId},{user.UserName},{user.RoomNumber}";
                lines.Add(line);
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
