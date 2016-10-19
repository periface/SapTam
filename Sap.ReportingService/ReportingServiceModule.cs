using Abp.Modules;
using System.Reflection;

namespace Sap.ReportingService
{
    public class ReportingServiceModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
