using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.Grab {
    public class GrabArguments {
        public string SearchPhrase { get; private set; }
        public string TargetFilePath { get; private set; }
        public Encoding Encoding { get; private set; }

        public GrabArguments(string searchPhrase, string filePath, Encoding encoding) {
            SearchPhrase = searchPhrase;
            TargetFilePath = filePath;
            Encoding = encoding;
        }
    }
}
