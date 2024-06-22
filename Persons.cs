namespace ElectronicEquipmentApp
{
    class Persons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public Persons(int id, string name, string passwordHash, bool isAdmin)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"{Id},{Name},{PasswordHash},{IsAdmin}";
        }
    }
    class Admin : Persons
    {
        public Admin(int id, string name, string passwordHash)
            : base(id, name, passwordHash, true)
        {
        }
    }
    class User : Admin
    {
        public User(int id, string name, string passwordHash)
            : base(id, name, passwordHash)
        {
            IsAdmin = false;
        }
    }
    class Employee : Persons
    {
        public string RoomNumber { get; set; }

        public Employee(int id, string name, string roomNumber)
            : base(id, name, null, false)  // Pracownik nie loguje się do programu (Jest tylko rekordem który można łączyć ze sprzętem.)
        {
            RoomNumber = roomNumber;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Imię i nazwisko: {Name}, Numer pokoju: {RoomNumber}";
        }
    }
}
