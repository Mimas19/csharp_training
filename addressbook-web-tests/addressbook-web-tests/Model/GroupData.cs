using LinqToDB.Mapping;

namespace addressbook_web_tests;

[Table(Name = "group_list")]
public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
{
    public bool Equals(GroupData other)
    {
        if (Object.ReferenceEquals(other, null))
        {
            return false;
        }

        if (Object.ReferenceEquals(this, other))
        {
            return true;
        }
        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override string ToString()
    {
        return "name=" + Name + "\nheader=" + Header + "\nfooter=" + Footer;
    }

    public int CompareTo(GroupData other)
    {
        if (Object.ReferenceEquals(other, null))
        {
            return 1;
        }
        return Name.CompareTo(other.Name);
    }

    public GroupData(string name)
    {
        Name = name;
    }
    public GroupData(string name, string header, string footer)
    {
        Name = name;
        Header = header;
        Footer = footer;
    }

    public GroupData()
    {
        
    }
    
    public static List<GroupData> GetAll()
    {
        using (AddressBookDB db = new AddressBookDB())
        {
            return (from g in db.Groups select g).ToList();
        }
    }
    
    [Column(Name = "group_name")] 
    public string Name { get; set; }
    
    [Column(Name = "group_header")] 
    public string Header { get; set; }
    
    [Column(Name = "group_footer")] 
    public string Footer { get; set; }
    
    [Column(Name = "group_id"), PrimaryKey, Identity] 
    public string Id { get; set; }

    public List<ContactData> GetContacts()
    {
        using (AddressBookDB db = new AddressBookDB())
        {
            return (from c in db.Contacts
                    from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)
                    select c).ToList();
        }
    }
    
}
