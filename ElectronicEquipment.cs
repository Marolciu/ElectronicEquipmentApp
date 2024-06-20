namespace ElectronicEquipmentApp
{
    class ElectronicEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee AssignedEmployee { get; set; }

        public ElectronicEquipment() { }

        public ElectronicEquipment(int id, string name, Employee assignedEmployee = null)
        {
            Id = id;
            Name = name;
            AssignedEmployee = assignedEmployee;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nazwa: {Name}, Pracownik: {(AssignedEmployee != null ? AssignedEmployee.Name : "Brak przypisanego pracownika")}";
        }
    }

    class Computer : ElectronicEquipment
    {
        public string CPU { get; set; }
        public int RAM { get; set; }

        public Computer(int id, string name, string cpu, int ram, Employee assignedEmployee = null)
            : base(id, name, assignedEmployee)
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

        public Monitor(int id, string name, int size, Employee assignedEmployee = null)
            : base(id, name, assignedEmployee)
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

        public Printer(int id, string name, string type, Employee assignedEmployee = null)
            : base(id, name, assignedEmployee)
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

        public Phone(int id, string name, string phoneNumber, Employee assignedEmployee = null)
            : base(id, name, assignedEmployee)
        {
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return base.ToString() + $", Numer telefonu: {PhoneNumber}";
        }
    }
}
