using System;
using System.Linq;
using Xunit;

namespace SpendingsSummary.ReportReader.Tests
{
    public class ReportReaderTests
    {
        [Fact]
        public void GetLines_From10LinesFile_Returns10LinesContent()
        {
            var report = new FileInFolderReport("TestFiles//Report1.csv");
            var repo = new ReportLinesRepository();
            var lines = repo.GetLines(report).Result;
            Assert.Equal(10, lines.Count());
        }
    }
}
