using System.IO;

namespace SpendingsSummary.ReportReader
{
    public static class FilesInFolder
    {
        public static string[] GetFileNames(string folderPath) => Directory.GetFiles(folderPath);
    }
}
