using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Configuration
{
    public static class Service
    {
        public static readonly RabbitMQ Value;

        private static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().Location;
                //var codeBase = AppContext.BaseDirectory;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static Service()
        {
            var assemblyConfigFile = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}{typeof(RabbitMQ).Name}.json";
            var configuration = new ConfigurationBuilder()
                // current dll config
                .AddJsonFile(assemblyConfigFile)
                .Build()
                ;
            Value = configuration.Get<RabbitMQ>();
        }
    }
}
