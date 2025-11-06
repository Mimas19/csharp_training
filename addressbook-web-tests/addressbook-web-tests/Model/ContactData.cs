namespace addressbook_web_tests;


public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
{
    private string allPhones;

    public ContactData(string contactName, string contactLastName, string homePhone, string mobilePhone, string workPhone, string email, string address)
    {
        Name = contactName;
        LastName = contactLastName;
        HomePhone = homePhone;
        MobilePhone = mobilePhone;
        WorkPhone = workPhone;
        Email = email;
        Address = address;
    }
    
    // Упрощённый конструктор (для GetContactList — только имя)
    public ContactData(string contactName)
    {
        Name = contactName;
        LastName = "";
        HomePhone = "";
        MobilePhone = "";
        WorkPhone = "";
        Email = "";
        Address = "";
    }
    
    public ContactData(string contactName, string contactLastName)
    {
        Name = contactName;
        LastName = contactLastName;
        HomePhone = "";
        MobilePhone = "";
        WorkPhone = "";
        Email = "";
        Address = "";
    }
    
    // консруктор с 5 параметрами 
    public ContactData(string contactName, string contactLastName, string phone, string email, string address)
    {
        Name = contactName;
        LastName = contactLastName;
        MobilePhone = phone;   // при желании можно назвать просто Phone
        Email = email;
        Address = address;

        HomePhone = "";        // или null, если нужно
        WorkPhone = "";
    }

   public string Name { get; set; }
   public string LastName { get; set; }
   public string HomePhone { get; set; }
   public string MobilePhone { get; set; }
   public string WorkPhone { get; set; }

   public string AllPhones
   {
       get
       {
           if (allPhones != null)
           {
               return allPhones;
           }
           else
           {
               return (CleanUp(HomePhone) + (MobilePhone) + (WorkPhone)).Trim();
           }
       }
       set
       {
           allPhones = value;
       }
   }

   private string CleanUp(string phone)
   {
       if (phone == null || phone == "")
       {
           return "";
       }
       return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
   }
   public string Email { get; set; }
   public string Address { get; set; }
   public string Id { get; set; }
   
   public bool Equals(ContactData other)
    {
        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        // Сравниваем по имени и фамилии
        return Name == other.Name
        && LastName == other.LastName
        && HomePhone == other.HomePhone
        && MobilePhone == other.MobilePhone
        && WorkPhone == other.WorkPhone
        && Email == other.Email
        && Address == other.Address;

    }

    public override int GetHashCode()
    {
        return (Name + LastName).GetHashCode();
    }

    public override string ToString()
    {
        return $"Name={Name}, LastName={LastName}";
    }
    public int CompareTo(ContactData other)
    {
        if (ReferenceEquals(other, null))
            return 1;

        int lastNameComparison = LastName.CompareTo(other.LastName);
        if (lastNameComparison != 0)
            return lastNameComparison;

        return Name.CompareTo(other.Name);
    }
    
}