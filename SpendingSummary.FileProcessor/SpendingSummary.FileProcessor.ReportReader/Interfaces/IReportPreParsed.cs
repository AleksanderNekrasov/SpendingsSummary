using System.IO;

namespace SpendingsSummary.ReportReader.Interfaces
{
    public interface IReportPreParsed
    {
        TextReader GetReader();
    }
}
