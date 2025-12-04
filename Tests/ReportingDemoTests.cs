using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;
using NUnit.Framework;

namespace EhuWebTests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
[AllureNUnit]
[AllureSuite("Allure Reporting Demo")]
[Category("Allure Reporting Demo")]
public class ReportingDemoTests
{
    [Test]
    [AllureFeature("Passing Scenario")]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureDescription("Simple assertion that is expected to pass.")]
    public void PassingScenario_ShouldSucceed()
    {
        var expected = 42;
        var actual = 40 + 2;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [AllureFeature("Failing Scenario")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Intentional failure to ensure Allure captures failed tests.")]
    public void FailingScenario_ShouldFail()
    {
        Assert.Fail("Intentional failure to demonstrate Allure FAIL status.");
    }

    [Test]
    [AllureFeature("Skipped Scenario")]
    [AllureSeverity(SeverityLevel.minor)]
    [AllureDescription("This test is skipped to satisfy the assignment requirement.")]
    public void SkippedScenario_ShouldBeSkipped()
    {
        Assert.Ignore("Skipped to demonstrate SKIP status in Allure report.");
    }
}

