using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using LinqToDB.Mapping;


namespace addressbook_web_tests;

[Table(Name = "addressbook")]
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
    

   [Column(Name = "firstname")] 
   public string Name { get; set; }
   
   [Column(Name = "lastname")] 
   public string LastName { get; set; }
   
   [Column(Name = "home")] 
   public string HomePhone { get; set; }
   
   [Column(Name = "mobile")] 
   public string MobilePhone { get; set; }
   
   [Column(Name = "work")] 
   public string WorkPhone { get; set; }
   
   
   public string Email1 { get; set; }
   public string Email2 { get; set; }
   
   [Column(Name = "email")] 
   public string Email { get; set; }
   
   [Column(Name = "address")] 
   public string Address { get; set; }
   
   [Column(Name = "id"), PrimaryKey, Identity] 
   public string Id { get; set; }
   
   
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
   
   private string allEmails;

   public string AllEmails
   {
       get
       {
           if (allEmails != null)
           {
               return allEmails;
           }
           else
           {
               return (CleanUpEmail(Email) + CleanUpEmail(Email1) + CleanUpEmail(Email2)).Trim();
           }
       }
       set
       {
           allEmails = value;
       }
   }

   private string CleanUpEmail(string email)
   {
       if (string.IsNullOrEmpty(email))
       {
           return "";
       }
       return email.Trim() + "\r\n";  // Добавляем перенос аналогично телефонов для удобства сравнения
   }


   private string CleanUp(string phone)
   {
       if (phone == null || phone == "")
       {
           return "";
       }
       return Regex.Replace(phone, "[ -()]", "") + "\r\n"; // регулырное выражение лекция5.4
   }
   
   
   public bool Equals(ContactData other)
    {
        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        // Сравниваем по имени и фамилии
        return Name == other.Name
        && LastName == other.LastName
        //&& HomePhone == other.HomePhone
        //&& MobilePhone == other.MobilePhone
        //&& WorkPhone == other.WorkPhone
        && Email == other.Email
        //&& Email1 == other.Email1
        //&& Email2 == other.Email2
        && Address == other.Address;

    }

    public override int GetHashCode()
    {
        return (Name + LastName).GetHashCode();
    }

    public override string ToString()
    {
        return "Name=" + Name +
               "\nLastName=" + LastName +
               "\nHomePhone=" + HomePhone +
               "\nMobilePhone=" + MobilePhone +
               "\nWorkPhone=" + WorkPhone +
               "\nEmail=" + Email +
               "\nEmail1=" + Email1 +
               "\nEmail2=" + Email2 +
               "\nAddress=" + Address +
               "\nAllPhones=" + AllPhones +
               "\nAllEmails=" + AllEmails;
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
    
    public ContactData()
    {
        // Пустой конструктор нужен для XML сериализации
    }

    public static List<ContactData> GetAll()
    {
        using (AddressBookDB db = new AddressBookDB())
        {
            return (from c in db.Contacts select c).ToList();
        }
    }

}