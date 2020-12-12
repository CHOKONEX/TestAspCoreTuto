using WebAppAspectCore.Models;

namespace WebAppAspectCore.Services
{
    public interface IService
    {
        [InterceptAttribute]
        string GetValue([ArgNotNullAttribute] string val);
    }

    public class Service : IService
    {
        [InterceptAttribute]
        //[ScopeIntercept(Scope = Scope.Nested)]
        [ParamNotNullAttribute]
        public string GetValue([ArgNotNullAttribute] string val)
        {
            return val;
        }
    }
}
