using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib.Tests
{
    public class TestHelper
    {
        public static string GetResourceFileContent(string assemblyName, string fileName)
        {
            var assembly = Assembly.GetCallingAssembly();
            var resourceName = $"{assemblyName}.{fileName}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new Exception($"Resource {resourceName} was not found.");
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
