using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpendingSummary.Common
{
    public static class EnvFile
    {
        public static void SetEnvironmentalVariablesFromEnvFile()
        {
            string envFilePath = ".env";
            string fullEnvPath = Path.Combine($"{Environment.CurrentDirectory}", envFilePath);
#if DEBUG
            var debugFolder = Directory.GetDirectories($"{Environment.CurrentDirectory}/bin/Debug")[0];
            fullEnvPath = Path.Combine(debugFolder, envFilePath);
#endif
            Read(fullEnvPath).ToList().ForEach(x =>
            {
                Environment.SetEnvironmentVariable(x.key, x.value);
            });
        }

        private static IEnumerable<(string key, string value)> Read(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            return File.ReadAllLines(filePath).Select(ParseLine);
        }

        private static (string key, string value) ParseLine(string line) 
        {
            var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) throw new EnvFileFormatException();
            return (parts[0], parts[1]);
        }
    }
}
