using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Asp.Core.Common.AssemblyFileReader
{
    public interface IAssemblyResourceReader
    {
        Task<IReadOnlyDictionary<string, string>> GetResourcesContent(Assembly assembly, IEnumerable<string> resourceNames);
    }
}