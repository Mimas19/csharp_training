namespace addressbook_web_tests;


public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
{
    public ContactData(string contactName, string contactLastName, string contactPhone, string contactEmail, string contactAddress)
    {
        Name = contactName;
        LastName = contactLastName;
        Phone = contactPhone;
        Email = contactEmail;
        Address = contactAddress;
    }
    
    // Упрощённый конструктор (для GetContactList — только имя)
    public ContactData(string contactName)
    {
        Name = contactName;
        LastName = "";
        Phone = "";
        Email = "";
        Address = "";
    }
   public string Name { get; set; }
   public string LastName { get; set; }
   public string Phone { get; set; }
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
        return Name == other.Name && LastName == other.LastName;
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