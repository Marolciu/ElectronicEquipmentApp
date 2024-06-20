namespace ElectronicEquipmentApp
{
    class User : Admin
    {
        public User(int id, string name, string passwordHash)
            : base(id, name, passwordHash)
        {
            IsAdmin = false;
        }
    }
}
