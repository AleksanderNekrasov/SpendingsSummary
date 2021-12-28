using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImportedReports.Parser.ReportParser.Interfaces
{
    public interface IReportLinesRepository
    {
        Task<IEnumerable<string>> GetLines(string fileName);

        Task<string> GetHeader(string fileName);
    }
}