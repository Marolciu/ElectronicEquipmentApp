namespace ElectronicEquipmentApp
{
    class Admin
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; } // Domyślnie true, bo to Admin

        public Admin(int adminId, string adminName, string passwordHash, bool isAdmin = true)
        {
            AdminId = adminId;
            AdminName = adminName;
            PasswordHash = passwordHash;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"{AdminId},{AdminName},{PasswordHash},{IsAdmin}";
        }
    }
}
