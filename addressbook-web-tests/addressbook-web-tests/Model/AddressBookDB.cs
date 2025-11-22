using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;


namespace addressbook_web_tests;

public class AddressBookDB : LinqToDB.Data.DataConnection
{
    public AddressBookDB() 
        : base(LinqToDB.ProviderName.MySql, 
            "Server=localhost;Port=3306;Database=addressbook;Uid=root;Pwd=;charset=utf8;Allow Zero Datetime=true")
    {
    }

    public ITable<GroupData> Groups {get {return this.GetTable<GroupData>();}}
    public ITable<ContactData> Contacts {get {return this.GetTable<ContactData>();}}
    public ITable<GroupContactRelation> GCR {get {return this.GetTable<GroupContactRelation>();}}
}