using System;
using System.Collections.Generic;
using System.IO;
using addressbooktests;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];
            if (dataType == "group")
            {
                GenerateGroupDataFile(count, fileName, format);
            }
            else if (dataType == "contact")
            {
                GenerateContactDataFile(count, fileName, format);
            }
            else
            {
                Console.Out.Write("Unrecognized data type: " + dataType);
            }
        }

        private static void GenerateGroupDataFile(int count, string fileName, string format)
        {
            List<GroupData> groups = new();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(30))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100),
                });
            }
            if (format == "excel")
            {
                WriteGroupsToExcelFile(groups, fileName);
            }
            else
            {
                StreamWriter writer = new(fileName);
                switch (format)
                {
                    case "csv":
                        WriteGroupsToCsvFile(groups, writer);
                        break;
                    case "xml":
                        WriteGroupsToXmlFile(groups, writer);
                        break;
                    case "json":
                        WriteGroupsToJsonFile(groups, writer);
                        break;
                    default:
                        Console.Out.Write("Unrecognized format: " + format);
                        break;
                }
                writer.Close();
            }
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(group.Name, group.Header, group.Footer);
            }
        }
        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }
        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }
        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application app = new();
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            wb.SaveAs2(fullPath);
            wb.Close();
            app.Quit();
        }
        private static void GenerateContactDataFile(int count, string fileName, string format)
        {
            List<ContactData> contacts = new();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(30))
                {
                    FirstName = TestBase.GenerateRandomString(100),
                    MiddleName = TestBase.GenerateRandomString(100),
                    LastName = TestBase.GenerateRandomString(100),
                    Title = TestBase.GenerateRandomString(100),
                    Company = TestBase.GenerateRandomString(100),
                    Address = TestBase.GenerateRandomString(100),
                    Home = TestBase.GenerateRandomIntString(10000),
                    Mobile = TestBase.GenerateRandomIntString(10000),
                    Email = TestBase.GenerateRandomString(100),
                });
            }
            StreamWriter writer = new(fileName);
            switch (format)
            {
                case "xml":
                    WriteContactsToXmlFile(contacts, writer);
                    break;
                case "json":
                    WriteContactsToJsonFile(contacts, writer);
                    break;
                default:
                    Console.Out.Write("Unrecognized format: " + format);
                    break;
            }
            writer.Close();
        }
        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }
    }
}
