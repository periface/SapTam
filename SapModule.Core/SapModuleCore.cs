using Abp.Modules;
using Abp.Zero;
using System.Reflection;

namespace SapModule.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SapModuleCore : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
