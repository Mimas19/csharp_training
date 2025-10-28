namespace addressbook_web_tests;


public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
{
    private string contactName;
    private string contactLastName;
    private string contactPhone;
    private string contactEmail;
    private string contactAddress;
    
    public ContactData(string contactName, string contactLastName, string contactPhone, string contactEmail, string contactAddress)
    {
        this.contactName = contactName;
        this.contactLastName = contactLastName;
        this.contactPhone = contactPhone;
        this.contactEmail = contactEmail;
        this.contactAddress = contactAddress;
        
    }
    
    // Упрощённый конструктор (для GetContactList — только имя)
    public ContactData(string contactName)
    {
        this.contactName = contactName;
        this.contactLastName = "";
        this.contactPhone = "";
        this.contactEmail = "";
        this.contactAddress = "";
    }
    public string Name
    {
        get
        {
            return contactName;
        }
        set
        {
            contactName = value;
        }
    }

    public string LastName
    {
        get
        {
            return contactLastName;
        }
        set
        {
            contactLastName = value;
        }
    }
    
    public string Phone
    {
        get
        {
            return contactPhone;
        }
        set
        {
            contactPhone = value;
        }
    }
    
    public string Email
    {
        get
        {
            return contactEmail;
        }
        set
        {
            contactEmail = value;
        }
    }
    
    public string Address
    {
        get
        {
            return contactAddress;
        }
        set
        {
            contactAddress = value;
        }
    }
    
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