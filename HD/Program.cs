using System;
using System.Collections.Generic;
using System.Linq;

namespace HD
{
    class Program
    {
        static void Main(string[] args)
        {
            string xml = @"<Students>
                <Student ID=""100"">
                    <Name>Arul</Name>
                    <Mark>90</Mark>
                </Student>
                <Student>
                    <Name>Arul2</Name>
                    <Mark>80</Mark>
                </Student>
            </Students>";

            dynamic students = DynamicXml.Parse(xml);            

            var id = students.Student[0].ID;
            var name1 = students.Student[1].Name;

            foreach (var std in students.Student)
            {
                //Console.WriteLine(std.ID);
                Console.WriteLine(std.Name);
                Console.WriteLine(std.Mark);
            }
        }
    }
}
