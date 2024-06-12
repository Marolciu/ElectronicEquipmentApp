namespace ElectronicEquipmentApp
{
    class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoomNumber { get; set; }

        public User(int userId, string userName, string roomNumber)
        {
            UserId = userId;
            UserName = userName;
            RoomNumber = roomNumber;
        }

        public override string ToString()
        {
            return $"ID: {UserId}, Imię i nazwisko: {UserName}, Numer pokoju: {RoomNumber}";
        }
    }
}
