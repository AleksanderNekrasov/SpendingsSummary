using System.IO;

namespace SpendingsSummary.Interfaces
{
    public interface IReportPreParsed
    {
        TextReader GetReader();
    }
}