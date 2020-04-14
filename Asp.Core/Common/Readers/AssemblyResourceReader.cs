using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Asp.Core.Common.AssemblyFileReader
{
    public class AssemblyResourceReader : IAssemblyResourceReader
    {
        public async Task<IReadOnlyDictionary<string, string>> GetResourcesContent(Assembly assembly, IEnumerable<string> resourceNames)
        {
            if (resourceNames == null)
            {
                throw new ArgumentNullException(nameof(resourceNames));
            }

            Dictionary<string, string> dict = new Dictionary<string, string>(); 
            foreach (string resourceName in resourceNames)
            {
                dict.Add(resourceName, await ReadFileAsync(assembly, resourceName));
            }
            return dict;
        }

        private async Task<string> ReadFileAsync(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
