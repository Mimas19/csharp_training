using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using addressbook_web_tests;
using Microsoft.VisualBasic;
using System.Xml;
using System.Xml.Serialization;

namespace addressbook_test_data_generators
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Проверка наличия нужных аргументов
            if (args.Length < 4)
            {
                Console.WriteLine("Ошибка: необходимо 4 аргумента: count filename format datatype");
                return;
            }

            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2].ToLower();    // "csv" или "xml"
            string datatype = args[3].ToLower();  // "group" или "contact"

            if (datatype == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(100),
                        Footer = TestBase.GenerateRandomString(100)
                    });
                }

                if (format == "csv")
                {
                    using (StreamWriter writer = new StreamWriter(filename))
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                }
                else if (format == "xml")
                {
                    WriteGroupsToXmlFile(groups, filename);
                }
                else
                {
                    Console.WriteLine("Ошибка: неизвестный формат " + format);
                }
            }
            else if (datatype == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(30),
                        TestBase.GenerateRandomString(50)
                    ));
                }

                if (format == "csv")
                {
                    using (StreamWriter writer = new StreamWriter(filename))
                    {
                        WriteContactsToCsvFile(contacts, writer);
                    }
                }
                else if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, filename);
                }
                else
                {
                    Console.WriteLine("Ошибка: неизвестный формат " + format);
                }
            }
            else
            {
                Console.WriteLine("Ошибка: неизвестный тип данных " + datatype);
            }
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine($"{group.Name},{group.Header},{group.Footer}");
            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                new XmlSerializer(typeof(List<GroupData>)).Serialize(stream, groups);
            }
        }

        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine($"{contact.Name},{contact.LastName},{contact.HomePhone},{contact.MobilePhone},{contact.WorkPhone},{contact.Email},{contact.Address}");
            }
        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                new XmlSerializer(typeof(List<ContactData>)).Serialize(stream, contacts);
            }
        }
    }
}