using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;

namespace EhuWebTests.Tests;

/// <summary>
/// Generates Allure HTML report automatically after all tests complete.
/// </summary>
[SetUpFixture]
public class AllureReportGenerator
{
    private static DateTime _testSuiteStartTime;

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        _testSuiteStartTime = DateTime.Now;
        Console.WriteLine($"=== Test Suite Started: {_testSuiteStartTime:yyyy-MM-dd HH:mm:ss} ===");
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        var testSuiteEndTime = DateTime.Now;
        var duration = testSuiteEndTime - _testSuiteStartTime;

        Console.WriteLine($"=== Test Suite Finished: {testSuiteEndTime:yyyy-MM-dd HH:mm:ss} ===");
        Console.WriteLine($"=== Total Duration: {duration.TotalSeconds:F2} seconds ===");

        GenerateAllureReport();
    }

    private void GenerateAllureReport()
    {
        var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
                          ?? Directory.GetCurrentDirectory();
        
        var allureResultsPath = Path.Combine(assemblyDir, "allure-results");
        var allureReportPath = Path.Combine(assemblyDir, "allure-report");

        if (!Directory.Exists(allureResultsPath))
        {
            Console.WriteLine($"[AllureReportGenerator] No allure-results found at: {allureResultsPath}");
            return;
        }

        Console.WriteLine($"[AllureReportGenerator] Generating HTML report...");
        Console.WriteLine($"  Results: {allureResultsPath}");
        Console.WriteLine($"  Output:  {allureReportPath}");

        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "allure",
                Arguments = $"generate \"{allureResultsPath}\" -o \"{allureReportPath}\" --clean",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processInfo);
            if (process != null)
            {
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"[AllureReportGenerator] Report generated successfully!");
                    Console.WriteLine($"[AllureReportGenerator] Open: {Path.Combine(allureReportPath, "index.html")}");
                }
                else
                {
                    Console.WriteLine($"[AllureReportGenerator] Allure command failed: {error}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AllureReportGenerator] Could not run 'allure' command: {ex.Message}");
            Console.WriteLine("[AllureReportGenerator] Make sure Allure CLI is installed and in PATH.");
            Console.WriteLine("[AllureReportGenerator] You can manually run: allure generate <results> -o <report>");
        }
    }
}

