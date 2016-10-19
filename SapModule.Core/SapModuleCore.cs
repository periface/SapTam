using Abp.Modules;
using Abp.Zero;
using Cinotam.AbpModuleZero;
using System.Reflection;

namespace SapModule.Core
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpModuleZeroCoreModule))]
    public class SapModuleCore : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
