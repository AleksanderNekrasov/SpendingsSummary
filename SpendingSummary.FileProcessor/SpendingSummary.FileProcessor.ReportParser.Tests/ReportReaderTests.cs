using System.Linq;
using Xunit;

namespace SpendingsSummary.ReportReader.Tests
{
    public class ReportReaderTests
    {
        [Fact]
        public void GetLines_From10LinesFile_Returns10LinesContent()
        {
            var repo = new ReportLinesRepository();
            var lines = repo.GetLines("TestFiles//Report1.csv").Result;
            Assert.Equal(10, lines.Count());
        }
    }
}
