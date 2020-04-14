using Asp.Core.Common.AssemblyFileReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace App.Core.Infra.FileResourceReader
{
    public class SqlFileQueryReader
    {
        private readonly string AssemblyName;
        private IReadOnlyDictionary<string, string> Queries;
        readonly Assembly assembly = Assembly.GetExecutingAssembly();

        private readonly IAssemblyResourceReader _assemblyResourceReader;
        
        public SqlFileQueryReader(IAssemblyResourceReader assemblyResourceReader)
        {
            _assemblyResourceReader = assemblyResourceReader;
            AssemblyName = assembly.GetName().Name;
            GetQueries();
        }

        private void GetQueries()
        {
            IEnumerable<string> sqlResources = assembly.GetManifestResourceNames().Where(x => x.EndsWith(".sql"));
            Queries = _assemblyResourceReader.GetResourcesContent(assembly, sqlResources).Result;
        }

        public string GetQuery(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (!fileName.EndsWith(".sql"))
            {
                throw new ArgumentException($"{fileName} is not an sql file");
            }

            string fileToSearch = fileName.Replace(".sql", "");
            if (fileToSearch.Contains("."))
            {
                if (!fileName.StartsWith(AssemblyName))
                {
                    fileName = $"{AssemblyName}.{fileName}";
                }

                return SearchByResourceName(fileName);
            }
            else
            {
                return SearchFilesThatEndsBy(fileName);
            }
        }

        private string SearchByResourceName(string fileName)
        {
            if (!Queries.TryGetValue(fileName, out string value))
            {
                throw new FileNotFoundException($"Embedded file {fileName} could not be found  in assembly {AssemblyName}.");
            }
            return value;
        }

        private string SearchFilesThatEndsBy(string fileName)
        {
            IEnumerable<KeyValuePair<string, string>> resources = Queries.Where(x => x.Key.EndsWith(fileName));
            if (resources.Count() == 0)
            {
                throw new FileNotFoundException($"Embedded file {fileName} could not be found in assembly {AssemblyName}.");
            }
            if (resources.Count() > 1)
            {
                throw new FileNotFoundException($"Embedded file {fileName} was found multiple times in {string.Join(", ", resources)}");
            }

            return resources.First().Value;
        }
    }
}
