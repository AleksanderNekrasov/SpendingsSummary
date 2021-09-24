using SpendingsSummary.ReportReader.Interfaces;
using System.IO;

namespace SpendingsSummary.ReportReader
{
    public class StringReport : IReportPreParsed
    {
        private string _textToParse;

        public StringReport(string textToParse)
        {
            _textToParse = textToParse;
        }

        public TextReader GetReader()
        {
            return new StringReader(_textToParse);
        }
    }
}
