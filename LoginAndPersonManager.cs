using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicEquipmentApp
{
    class LoginAndPersonManager
    {
        private string connectionString = "Data Source=equipmentApp.db;Version=3;";
        private List<Persons> personList;

        public LoginAndPersonManager()
        {
            CreateDatabase();
            personList = ReadPersonsFromDatabase();
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Persons (
                                            Id INTEGER PRIMARY KEY,
                                            Name TEXT,
                                            PasswordHash TEXT,
                                            IsAdmin INTEGER)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private List<Persons> ReadPersonsFromDatabase()
        {
            List<Persons> persons = new List<Persons>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Persons";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string passwordHash = reader.GetString(2);
                        bool isAdmin = reader.GetInt32(3) == 1;

                        if (isAdmin)
                        {
                            persons.Add(new Admin(id, name, passwordHash));
                        }
                        else
                        {
                            persons.Add(new User(id, name, passwordHash));
                        }
                    }
                }
            }

            return persons;
        }

        private void WritePersonsToDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Persons";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                foreach (var person in personList)
                {
                    string insertQuery = "INSERT INTO Persons (Id, Name, PasswordHash, IsAdmin) VALUES (@Id, @Name, @PasswordHash, @IsAdmin)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", person.Id);
                        command.Parameters.AddWithValue("@Name", person.Name);
                        command.Parameters.AddWithValue("@PasswordHash", person.PasswordHash);
                        command.Parameters.AddWithValue("@IsAdmin", person.IsAdmin ? 1 : 0);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Register(bool isAdmin = true)
        {
            Console.WriteLine("Podaj ID (6 cyfr):");
            int id = int.Parse(Console.ReadLine());

            if (personList.Any(p => p.Id == id))
            {
                Console.WriteLine("Osoba o podanym ID już istnieje.");
                return;
            }

            Console.WriteLine("Podaj imię i nazwisko:");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj hasło:");
            string password = ReadPassword();

            string passwordHash = HashPassword(password);

            Persons person;
            if (isAdmin)
            {
                person = new Admin(id, name, passwordHash);
            }
            else
            {
                person = new User(id, name, passwordHash);
            }

            personList.Add(person);
            WritePersonsToDatabase();

            Console.WriteLine("Rejestracja zakończona sukcesem.");
        }

        public Persons Login()
        {
            Console.WriteLine("Podaj ID:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj hasło:");
            string password = ReadPassword();

            string passwordHash = HashPassword(password);
            Persons person = personList.FirstOrDefault(p => p.Id == id && p.PasswordHash == passwordHash);
            if (person == null)
            {
                Console.WriteLine("Nieprawidłowe ID lub hasło.");
            }

            return person;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }
    }
}
