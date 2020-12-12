using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace WebAppAspectCore.Models
{
    public class InterceptAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            context.Parameters[0] = "lemon";
            return context.Invoke(next);
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    [NonAspect]
    public class ParamNotNullAttribute : Attribute, IInterceptor
    {
        public bool AllowMultiple => true;

        bool IInterceptor.Inherited { get => true; set => value = true; }
        int IInterceptor.Order { get => 1; set { value = 1; } }

        public Task Invoke(AspectContext context, AspectDelegate next)
        {
            foreach(var param in context.Parameters)
            {
                if (param == null) throw new System.Exception("null");
            }
            return context.Invoke(next);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    [NonAspect]
    public class ArgNotNullAttribute : Attribute, IInterceptor
    {
        public bool AllowMultiple => true;

        bool IInterceptor.Inherited { get => true; set => value = true; }
        int IInterceptor.Order { get => 1; set { value = 1; } }

        public Task Invoke(AspectContext context, AspectDelegate next)
        {
            foreach (var param in context.Parameters)
            {
                if (param == null) throw new System.Exception("null");
            }
            return context.Invoke(next);
        }
    }

    //public class ScopeIntercept : ScopeInterceptorAttribute
    //{
    //    public override Scope Scope { get; set; } = Scope.None;

    //    public override Task Invoke(AspectContext context, AspectDelegate next)
    //    {
    //        //Console.WriteLine("trace id: {0} . execute method : {1}.{2}", GetTraceId(context), context.ServiceMethod.DeclaringType, context.ServiceMethod.Name);
    //        return context.Invoke(next);
    //    }

    //    private string GetTraceId(AspectContext currentContext)
    //    {
    //        var scheduler = (IAspectScheduler)currentContext.ServiceProvider.GetService(typeof(IAspectScheduler));
    //        var firstContext = scheduler.GetCurrentContexts().First();
    //        if (firstContext.AdditionalData.TryGetValue("trace-id", out var traceId))
    //        {
    //            return traceId.ToString();
    //        }
    //        traceId = Guid.NewGuid();
    //        firstContext.AdditionalData["trace-id"] = traceId;
    //        return traceId.ToString();
    //    }
    //}
}
