using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlayWrightDemo.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTests : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitle()
    {
        await Page.GotoAsync("https://playwright.dev");

        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
    }

    [Test]
    public async Task GetStartedLinkNavigatesToInstallation()
    {
        await Page.GotoAsync("https://playwright.dev");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
    }
}
