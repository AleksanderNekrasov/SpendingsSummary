using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpendingsSummary.Interfaces
{
    public interface IReportLinesRepository
    {
        Task<IEnumerable<string>> GetLines(string fileName);
    }
}