using System.IO;
using System.Text;
using System.Xml.Linq;

namespace HD.Commands {
    
    using HD.Commands.Contracts;
    using System.Linq;

    public class XMLCommand : IXMLCommand {

        string _property;
        FileStream _fileStream;

        public XMLCommand(string property, FileStream fileStream) {
            _property = property;
            _fileStream = fileStream;
        }

        public string Execute() {
            
            var stringReturn = new StringBuilder();

            var xmlDocument = XDocument.Load(_fileStream);

            // var Items = xmlDocument.Root.(_property);

            var Items = from el in xmlDocument.Descendants()
            where (string) el.Attribute("Text") == _property
            select el;

            foreach (var item in Items) {
                stringReturn.AppendLine(item.ToString());                
            }

            return stringReturn.ToString();
        }

        public void Redo() {
            throw new System.NotImplementedException();
        }

        public void Undo() {
            throw new System.NotImplementedException();
        }
    }
}
