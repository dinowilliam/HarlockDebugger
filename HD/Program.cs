using HD.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HD
{
    class Program {

        static void Main(string[] args) {

            if (args.Length > 1){                

                FileStream fs = new FileStream(args[0], FileMode.Open, FileAccess.Read);
                
                var xmlComand = new XMLCommand(args[1], fs);
                Console.WriteLine(xmlComand.Execute());

                Console.ReadLine();
        
            }
        }

    }
}
