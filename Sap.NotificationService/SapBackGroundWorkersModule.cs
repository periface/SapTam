using System.Reflection;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Sap.BackGroundWorkers.BackGroundWorkers;

namespace Sap.BackGroundWorkers
{
    public class SapBackGroundWorkersModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<CheckTodosDateTimeStatus>());
        }
    }
}
