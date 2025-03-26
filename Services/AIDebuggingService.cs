using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RTUI.Services
{
    public class AIDebuggingService
    {
        private readonly string _projectRoot;

        public AIDebuggingService(string projectRoot)
        {
            _projectRoot = projectRoot;
        }

        // Main method to scan project files and detect common errors
        public async Task<string> AnalyzeAndFixBugsAsync()
        {
            try
            {
                var files = Directory.GetFiles(_projectRoot, "*.cs", SearchOption.AllDirectories);

                // Analyze each file for common issues (example: missing try-catch blocks)
                foreach (var file in files)
                {
                    string code = await File.ReadAllTextAsync(file);

                    // Example check for missing try-catch around database calls
                    if (code.Contains("DbContext") && !code.Contains("try"))
                    {
                        code = AddTryCatchToDbContextCalls(code);
                        await File.WriteAllTextAsync(file, code);
                    }
                }

                return "Bug detection and fixes applied!";
            }
            catch (Exception ex)
            {
                return $"Error during bug detection: {ex.Message}";
            }
        }

        // Example logic: Auto-adds try-catch blocks to database calls
        private string AddTryCatchToDbContextCalls(string code)
        {
            // Adds a try-catch block around methods using DbContext
            string updatedCode = code.Replace(
                "DbContext",
                "try { DbContext"
            ).Replace("SaveChanges();", "} catch (Exception ex) { Console.WriteLine(ex.Message); throw; } SaveChanges();");

            return updatedCode;
        }
    }
}
