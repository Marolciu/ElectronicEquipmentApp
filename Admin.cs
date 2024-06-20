namespace ElectronicEquipmentApp
{
    class Admin : Person
    {
        public Admin(int id, string name, string passwordHash)
            : base(id, name, passwordHash, true)
        {
        }
    }
}
