namespace ElectronicEquipmentApp
{
    class Employee : Person
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
