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

namespace addressbook_test_data_generators;

public class Program
{
    static void Main(string[] args)
    {
        int count = Convert.ToInt32(args[0]);
        StreamWriter writer = new StreamWriter(args[1]);
        string format = args[3]; 
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
            writeGroupsToCsvFile(groups, writer);
        }
        else if (format == "xml")
        {
            writeGroupsToXmlFile(groups, writer);
        }
        else
        {
            System.Console.Out.Write("Unknown format" + format);
        }
        writer.Close();
    }

    static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
    {
        foreach (GroupData group in groups)
        {
            writer.WriteLine(String.Format("${0},${1},${2}",
                group.Name, group.Header, group.Footer));
        }
    }

    static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
    {
        new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
    }
}