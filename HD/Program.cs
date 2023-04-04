using System;
using System.IO;

namespace HD {

    using HD.Commands;

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
