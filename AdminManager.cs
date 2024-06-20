using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicEquipmentApp
{
    class AdminManager
    {
        private string filePath = "persons.txt";
        private List<Person> personList;

        public AdminManager()
        {
            personList = ReadPersonsFromFile();
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

            Person person;
            if (isAdmin)
            {
                person = new Admin(id, name, passwordHash);
            }
            else
            {
                person = new User(id, name, passwordHash);
            }

            personList.Add(person);
            WritePersonsToFile();

            Console.WriteLine("Rejestracja zakończona sukcesem.");
        }

        public Person Login()
        {
            Console.WriteLine("Podaj ID:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj hasło:");
            string password = ReadPassword();

            string passwordHash = HashPassword(password);
            Person person = personList.FirstOrDefault(p => p.Id == id && p.PasswordHash == passwordHash);
            if (person == null)
            {
                Console.WriteLine("Nieprawidłowe ID lub hasło.");
            }

            return person;
        }

        private List<Person> ReadPersonsFromFile()
        {
            List<Person> persons = new List<Person>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        string passwordHash = parts[2];
                        bool isAdmin = bool.Parse(parts[3]);

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

        private void WritePersonsToFile()
        {
            List<string> lines = new List<string>();

            foreach (Person person in personList)
            {
                lines.Add(person.ToString());
            }

            File.WriteAllLines(filePath, lines);
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
