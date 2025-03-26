using Microsoft.AspNetCore.Mvc;
using RTUI.Services;
using System.Threading.Tasks;

namespace RTUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AIDebuggingService _aiDebuggingService;

        public DashboardController(AIDebuggingService aiDebuggingService)
        {
            _aiDebuggingService = aiDebuggingService;
        }

        // Action to trigger AI-based debugging from the dashboard
        [HttpPost]
        public async Task<IActionResult> RunAIDebugger()
        {
            string result = await _aiDebuggingService.AnalyzeAndFixBugsAsync();
            ViewBag.DebugResult = result;  // Display the result in the UI
            return View("Dashboard");  // Redirect to the main dashboard
        }
    }
}
