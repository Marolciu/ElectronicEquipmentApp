using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace ElectronicEquipmentApp
{
    class ProgramManager
    {
        private string connectionString = "Data Source=equipmentApp.db;Version=3;";

        public List<Employee> EmployeeList { get; private set; } = new List<Employee>();
        public List<ElectronicEquipment> EquipmentList { get; private set; } = new List<ElectronicEquipment>();

        public ProgramManager()
        {
            CreateDatabase();
            EmployeeList = ReadEmployeesFromDatabase();
            EquipmentList = ReadEquipmentFromDatabase();
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createEmployeeTableQuery = @"CREATE TABLE IF NOT EXISTS Employees (
                                                    Id INTEGER PRIMARY KEY,
                                                    Name TEXT,
                                                    RoomNumber TEXT)";
                string createEquipmentTableQuery = @"CREATE TABLE IF NOT EXISTS Equipment (
                                                    Id INTEGER PRIMARY KEY,
                                                    Name TEXT,
                                                    Type TEXT,
                                                    AssignedEmployeeId INTEGER,
                                                    CPU TEXT,
                                                    RAM INTEGER,
                                                    Size INTEGER,
                                                    PrinterType TEXT,
                                                    PhoneNumber TEXT)";
                using (var command = new SQLiteCommand(createEmployeeTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createEquipmentTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private List<Employee> ReadEmployeesFromDatabase()
        {
            List<Employee> employees = new List<Employee>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Employees";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string roomNumber = reader.GetString(2);

                        employees.Add(new Employee(id, name, roomNumber));
                    }
                }
            }

            return employees;
        }

        private List<ElectronicEquipment> ReadEquipmentFromDatabase()
        {
            List<ElectronicEquipment> equipment = new List<ElectronicEquipment>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Equipment";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string type = reader.GetString(2);
                        int? assignedEmployeeId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);

                        Employee assignedEmployee = assignedEmployeeId.HasValue ? EmployeeList.FirstOrDefault(e => e.Id == assignedEmployeeId.Value) : null;

                        ElectronicEquipment equipmentItem = null;
                        switch (type.ToLower())
                        {
                            case "computer":
                                string cpu = reader.GetString(4);
                                int ram = reader.GetInt32(5);
                                equipmentItem = new Computer(id, name, cpu, ram, assignedEmployee);
                                break;
                            case "monitor":
                                int size = reader.GetInt32(6);
                                equipmentItem = new Monitor(id, name, size, assignedEmployee);
                                break;
                            case "printer":
                                string printerType = reader.GetString(7);
                                equipmentItem = new Printer(id, name, printerType, assignedEmployee);
                                break;
                            case "phone":
                                string phoneNumber = reader.GetString(8);
                                equipmentItem = new Phone(id, name, phoneNumber, assignedEmployee);
                                break;
                        }

                        equipment.Add(equipmentItem);
                    }
                }
            }

            return equipment;
        }

        public void WriteEmployeesToDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Employees";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                foreach (var employee in EmployeeList)
                {
                    string insertQuery = "INSERT INTO Employees (Id, Name, RoomNumber) VALUES (@Id, @Name, @RoomNumber)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employee.Id);
                        command.Parameters.AddWithValue("@Name", employee.Name);
                        command.Parameters.AddWithValue("@RoomNumber", employee.RoomNumber);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void WriteEquipmentToDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Equipment";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                foreach (var equipment in EquipmentList)
                {
                    string insertQuery = "INSERT INTO Equipment (Id, Name, Type, AssignedEmployeeId, CPU, RAM, Size, PrinterType, PhoneNumber) " +
                                         "VALUES (@Id, @Name, @Type, @AssignedEmployeeId, @CPU, @RAM, @Size, @PrinterType, @PhoneNumber)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", equipment.Id);
                        command.Parameters.AddWithValue("@Name", equipment.Name);
                        command.Parameters.AddWithValue("@Type", equipment.GetType().Name);
                        command.Parameters.AddWithValue("@AssignedEmployeeId", equipment.AssignedEmployee?.Id ?? (object)DBNull.Value);

                        if (equipment is Computer computer)
                        {
                            command.Parameters.AddWithValue("@CPU", computer.CPU);
                            command.Parameters.AddWithValue("@RAM", computer.RAM);
                            command.Parameters.AddWithValue("@Size", DBNull.Value);
                            command.Parameters.AddWithValue("@PrinterType", DBNull.Value);
                            command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        }
                        else if (equipment is Monitor monitor)
                        {
                            command.Parameters.AddWithValue("@CPU", DBNull.Value);
                            command.Parameters.AddWithValue("@RAM", DBNull.Value);
                            command.Parameters.AddWithValue("@Size", monitor.Size);
                            command.Parameters.AddWithValue("@PrinterType", DBNull.Value);
                            command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        }
                        else if (equipment is Printer printer)
                        {
                            command.Parameters.AddWithValue("@CPU", DBNull.Value);
                            command.Parameters.AddWithValue("@RAM", DBNull.Value);
                            command.Parameters.AddWithValue("@Size", DBNull.Value);
                            command.Parameters.AddWithValue("@PrinterType", printer.Type);
                            command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        }
                        else if (equipment is Phone phone)
                        {
                            command.Parameters.AddWithValue("@CPU", DBNull.Value);
                            command.Parameters.AddWithValue("@RAM", DBNull.Value);
                            command.Parameters.AddWithValue("@Size", DBNull.Value);
                            command.Parameters.AddWithValue("@PrinterType", DBNull.Value);
                            command.Parameters.AddWithValue("@PhoneNumber", phone.PhoneNumber);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CPU", DBNull.Value);
                            command.Parameters.AddWithValue("@RAM", DBNull.Value);
                            command.Parameters.AddWithValue("@Size", DBNull.Value);
                            command.Parameters.AddWithValue("@PrinterType", DBNull.Value);
                            command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AddEmployee()
        {
            try
            {
                Console.WriteLine("Podaj ID pracownika:");
                int employeeId = int.Parse(Console.ReadLine());

                if (EmployeeList.Any(u => u.Id == employeeId))
                {
                    Console.WriteLine("Pracownik o podanym ID już istnieje.");
                    return;
                }

                Console.WriteLine("Podaj imię i nazwisko:");
                string employeeName = Console.ReadLine();

                Console.WriteLine("Podaj numer pokoju:");
                string roomNumber = Console.ReadLine();

                Employee employee = new Employee(employeeId, employeeName, roomNumber);
                EmployeeList.Add(employee);
                WriteEmployeesToDatabase();
                Console.WriteLine("Pracownik został dodany.");
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

                Console.WriteLine("Podaj ID pracownika:");
                int employeeId = int.Parse(Console.ReadLine());

                Employee employee = EmployeeList.FirstOrDefault(u => u.Id == employeeId);
                if (employee == null)
                {
                    Console.WriteLine("Nie znaleziono pracownika o podanym ID.");
                    return;
                }

                equipment.AssignedEmployee = employee;
                WriteEquipmentToDatabase();
                Console.WriteLine("Sprzęt został przypisany pracownikowi.");
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
                WriteEquipmentToDatabase();
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

                    WriteEquipmentToDatabase();
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
                if (equipment.AssignedEmployee != null)
                {
                    Console.WriteLine($"Pracownik: {equipment.AssignedEmployee.Name}");
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
            string header = "| {0,-10} | {1,-18} | {2,-11} | {3,-20} | {4,-8} | {5,-4} | {6,-7} | {7,-15} | {8,-16} |";
            string separator = new string('-', 137);

            Console.WriteLine(separator);
            Console.WriteLine(header, "ID", "Nazwa", "Typ", "Pracownik", "CPU", "RAM", "Rozmiar", "Typ Drukarki", "Numer Telefonu");
            Console.WriteLine(separator);

            foreach (var equipment in equipmentList)
            {
                string equipmentType = equipment.GetType().Name;
                string employeeName = equipment.AssignedEmployee != null ? equipment.AssignedEmployee.Name : "Brak";
                string cpu = equipment is Computer computer ? computer.CPU : "";
                string ram = equipment is Computer computer2 ? computer2.RAM.ToString() : "";
                string size = equipment is Monitor monitor ? monitor.Size.ToString() : "";
                string printerType = equipment is Printer printer ? printer.Type : "";
                string phoneNumber = equipment is Phone phone ? phone.PhoneNumber : "";

                Console.WriteLine(header, equipment.Id, equipment.Name, equipmentType, employeeName, cpu, ram, size, printerType, phoneNumber);
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
                    WriteEquipmentToDatabase();
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
    }
}
