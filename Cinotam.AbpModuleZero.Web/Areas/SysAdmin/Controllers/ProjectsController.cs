using Cinotam.AbpModuleZero.Web.Controllers;
using SapModule.Application.Projects;
using SapModule.Application.Projects.Dto;
using System.Web.Mvc;

namespace Cinotam.AbpModuleZero.Web.Areas.SysAdmin.Controllers
{
    public class ProjectsController : AbpModuleZeroControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: SysAdmin/Projects
        public ActionResult ProjectsList()
        {
            return View();
        }
        [ChildActionOnly]
        public ViewResult Projects()
        {
            var projects = _projectService.GetProjects(1, 10, "");
            return View(projects);
        }
        public ActionResult Create()
        {
            return View(new ProjectInput());
        }

        public ActionResult Edit(int id)
        {
            var project = _projectService.GetProjectForEdit(id);
            return View(project);
        }
    }
}