namespace ElectronicEquipmentApp
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public Person(int id, string name, string passwordHash, bool isAdmin)
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
}
