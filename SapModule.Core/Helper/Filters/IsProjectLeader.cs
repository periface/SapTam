using Abp.Dependency;
using Abp.Logging;
using Abp.Runtime.Session;
using Abp.UI;
using SapModule.Core.Projects;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SapModule.Core.Helper.Filters
{
    /// <summary>
    /// For client validation
    /// </summary>
    /// 
    public class IsProjectLeader : ActionFilterAttribute
    {
        public IocManager IocManager { get; set; }
        public IAbpSession AbpSession { get; set; }
        private readonly IProjectManager _projectManager;
        private bool _isLeader;
        private const string IdValue = "id";
        public IsProjectLeader()
        {
            IocManager = IocManager.Instance;
            AbpSession = NullAbpSession.Instance;
            _projectManager = IocManager.Resolve<IProjectManager>();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var id = actionContext.ControllerContext.RouteData.Values[IdValue];
            int resolvedId;
            if (id != null)
            {
                resolvedId = (int)id;
            }
            else
            {
                throw new InvalidOperationException("Project id not present!");
            }
            _isLeader = _projectManager.CheckLeaderShip(resolvedId, AbpSession.UserId);
            if (_isLeader)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                throw new UserFriendlyException("Usted no es el lider de este proyecto", LogSeverity.Info);
            }
        }
    }
}
