using Abp.Modules;
using SapModule.Core;
using System.Reflection;

namespace SapModule.Application
{
    [DependsOn(typeof(SapModuleCore))]
    public class SapModuleApp : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
