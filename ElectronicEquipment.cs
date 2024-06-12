namespace ElectronicEquipmentApp
{
    class ElectronicEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User AssignedUser { get; set; } // Dodane pole

        public ElectronicEquipment() { }

        public ElectronicEquipment(int id, string name, User assignedUser = null)
        {
            Id = id;
            Name = name;
            AssignedUser = assignedUser;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nazwa: {Name}, Użytkownik: {(AssignedUser != null ? AssignedUser.UserName : "Brak przypisanego użytkownika")}";
        }
    }

    class Computer : ElectronicEquipment
    {
        public string CPU { get; set; }
        public int RAM { get; set; }

        public Computer(int id, string name, string cpu, int ram, User assignedUser = null)
            : base(id, name, assignedUser)
        {
            CPU = cpu;
            RAM = ram;
        }

        public override string ToString()
        {
            return base.ToString() + $", CPU: {CPU}, RAM: {RAM} GB";
        }
    }

    class Monitor : ElectronicEquipment
    {
        public int Size { get; set; }

        public Monitor(int id, string name, int size, User assignedUser = null)
            : base(id, name, assignedUser)
        {
            Size = size;
        }

        public override string ToString()
        {
            return base.ToString() + $", Rozmiar: {Size} cali";
        }
    }

    class Printer : ElectronicEquipment
    {
        public string Type { get; set; }

        public Printer(int id, string name, string type, User assignedUser = null)
            : base(id, name, assignedUser)
        {
            Type = type;
        }

        public override string ToString()
        {
            return base.ToString() + $", Typ drukarki: {Type}";
        }
    }

    class Phone : ElectronicEquipment
    {
        public string PhoneNumber { get; set; }

        public Phone(int id, string name, string phoneNumber, User assignedUser = null)
            : base(id, name, assignedUser)
        {
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return base.ToString() + $", Numer telefonu: {PhoneNumber}";
        }
    }
}
