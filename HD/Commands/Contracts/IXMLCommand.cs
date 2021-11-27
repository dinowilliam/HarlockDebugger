using System.IO;

namespace HD.Commands.Contracts {
    public interface IXMLCommand {
        string Execute();
        void Undo();
        void Redo();

    }
}
