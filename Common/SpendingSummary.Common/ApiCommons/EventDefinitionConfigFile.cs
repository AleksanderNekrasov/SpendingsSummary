using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SpendingSummary.Common.ApiCommons
{
    public static class EventDefinitionConfigFile
    {
        public static IConfigurationBuilder AddEventDefinitionConfigFile(this IConfigurationBuilder builder)
        {
            var path = "queue-events-definition.yml";
#if DEBUG
            var debugFolder = $"{Environment.CurrentDirectory}/bin/Debug/net6.0";
            path = Path.Combine(debugFolder, "queue-events-definition.yml");
#endif
            return builder.AddYamlFile(path);
        }
    }
}
