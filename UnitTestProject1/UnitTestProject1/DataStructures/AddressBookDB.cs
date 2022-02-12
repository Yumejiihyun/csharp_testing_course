using LinqToDB;

namespace addressbooktests
{
    class AddressBookDB : LinqToDB.Data.DataConnection
    {
        public AddressBookDB() : base("AddressBook") { }
        public ITable<GroupData> Groups { get => GetTable<GroupData>(); }
        public ITable<ContactData> Contacts { get => GetTable<ContactData>(); }
        public ITable<GroupContactRelation> GroupContactRelations { get => GetTable<GroupContactRelation>(); }
    }
}
