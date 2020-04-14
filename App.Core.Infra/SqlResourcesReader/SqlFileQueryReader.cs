using Asp.Core.Attributes;
using Asp.Core.Common.AssemblyFileReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace App.Core.Infra.SqlResourcesReader
{
    [Singleton]
    public class SqlFileQueryReader : ISqlFileQueryReader
    {
        private readonly string AssemblyName;
        private IReadOnlyDictionary<string, string> Queries;
        readonly Assembly assembly = Assembly.GetExecutingAssembly();

        private readonly IAssemblyResourceReader _assemblyResourceReader;

        public SqlFileQueryReader(IAssemblyResourceReader assemblyResourceReader)
        {
            _assemblyResourceReader = assemblyResourceReader ?? throw new ArgumentNullException(nameof(assemblyResourceReader));
            AssemblyName = assembly.GetName().Name;
            GetQueries();
        }

        private void GetQueries()
        {
            IEnumerable<string> sqlResources = assembly.GetManifestResourceNames().Where(x => x.EndsWith(".sql"));
            Queries = _assemblyResourceReader.GetResourcesContent(assembly, sqlResources).Result;
        }

        public string GetQuery(string sqlFileName)
        {
            if (string.IsNullOrWhiteSpace(sqlFileName))
            {
                throw new ArgumentNullException(nameof(sqlFileName));
            }
            if (!sqlFileName.EndsWith(".sql"))
            {
                throw new ArgumentException($"{sqlFileName} is not an sql file");
            }

            int countOccurences = sqlFileName.Count(x => x == '.');
            if (countOccurences > 1)
            {
                return SearchByLongResourceName(GetValidName(sqlFileName));
            }
            else
            {
                return SearchFilesThatEndsBy(sqlFileName);
            }
        }

        private string GetValidName(string sqlFileName)
        {
            if (!sqlFileName.StartsWith(AssemblyName))
            {
                sqlFileName = $"{AssemblyName}.{sqlFileName}";
            }
            return sqlFileName;
        }

        private string SearchByLongResourceName(string fileName)
        {
            if (!Queries.TryGetValue(fileName, out string value))
            {
                throw new FileNotFoundException($"Embedded file {fileName} could not be found in assembly {AssemblyName}.");
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
