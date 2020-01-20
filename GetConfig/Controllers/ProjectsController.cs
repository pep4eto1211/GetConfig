using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GetConfig.Db.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GetConfig.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ProjectsService _db;
        public ProjectsController()
        {
            this._db = new ProjectsService();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(this._db.GetProjectsForUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public IActionResult ConfigValues(int id)
        {
            var model = this._db.GetConfigValuesForProject(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.ProjectName = model.ProjectSettings.Name;
            ViewBag.ProjectId = id;
            return View(model);
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult CreateValue(string name, string description, string value, int project)
        {
            this._db.CreateConfigValue(name, description, value, project);
            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult EditValue(string name, string description, string value, int id)
        {
            this._db.EditConfigValue(name, description, value, id);
            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult DeleteValue(int id)
        {
            this._db.DeleteConfigValue(id);
            return Ok();
        }
    }
}
