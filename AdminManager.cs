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
        private string filePath = "admins.txt";
        private List<Admin> adminList;

        public AdminManager()
        {
            adminList = ReadAdminsFromFile();
        }

        public void Register()
        {
            Console.WriteLine("Podaj ID (6 cyfr):");
            int adminId = int.Parse(Console.ReadLine());

            if (adminList.Any(u => u.AdminId == adminId))
            {
                Console.WriteLine("Administrator o podanym ID już istnieje.");
                return;
            }

            Console.WriteLine("Podaj imię i nazwisko:");
            string adminName = Console.ReadLine();

            Console.WriteLine("Podaj hasło:");
            string password = ReadPassword();

            string passwordHash = HashPassword(password);

            Admin admin = new Admin(adminId, adminName, passwordHash);
            adminList.Add(admin);
            WriteAdminsToFile();

            Console.WriteLine("Rejestracja zakończona sukcesem.");
        }

        public Admin Login()
        {
            Console.WriteLine("Podaj ID:");
            int adminId = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj hasło:");
            string password = ReadPassword();

            string passwordHash = HashPassword(password);
            Admin admin = adminList.FirstOrDefault(u => u.AdminId == adminId && u.PasswordHash == passwordHash);
            if (admin == null)
            {
                Console.WriteLine("Nieprawidłowe ID lub hasło.");
            }

            return admin;
        }

        private List<Admin> ReadAdminsFromFile()
        {
            List<Admin> admins = new List<Admin>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        int adminId = int.Parse(parts[0]);
                        string adminName = parts[1];
                        string passwordHash = parts[2];
                        bool isAdmin = bool.Parse(parts[3]);
                        admins.Add(new Admin(adminId, adminName, passwordHash, isAdmin));
                    }
                }
            }

            return admins;
        }

        private void WriteAdminsToFile()
        {
            List<string> lines = new List<string>();

            foreach (Admin admin in adminList)
            {
                lines.Add(admin.ToString());
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
