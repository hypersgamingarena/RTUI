using Microsoft.AspNetCore.Mvc;
using RTUI.Models;

namespace RTUI.Controllers
{
    public class ProjectsController : Controller
    {
        private static List<Project> projects = new List<Project>(); // Temporary in-memory storage

        public IActionResult List()
        {
            try
            {
                return View(projects);  // Displays project list
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Unable to load projects. Please try again later.";
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    projects.Add(project);  // Add project to the list
                    TempData["SuccessMessage"] = "Project added successfully!";
                    return RedirectToAction("List");
                }
                return View(project);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error while adding the project. Please try again.";
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
