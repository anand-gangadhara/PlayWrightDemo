# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Playwright for .NET end-to-end test suite: `PlayWrightDemo.slnx` with one NUnit project, `PlayWrightDemo.Tests` (net10.0, `Microsoft.Playwright.NUnit`).

## Environment

The .NET SDK is installed user-locally in `~/.dotnet` (not on the default PATH), and PowerShell is a .NET global tool in `~/.dotnet/tools`. In a fresh shell:

```bash
export DOTNET_ROOT=~/.dotnet PATH=~/.dotnet:~/.dotnet/tools:$PATH
```

## Commands

Run from the repo root:

```bash
dotnet build
pwsh PlayWrightDemo.Tests/bin/Debug/net10.0/playwright.ps1 install chromium   # browsers; rerun after bumping the Playwright package
dotnet test --settings .runsettings
dotnet test --settings .runsettings --filter "FullyQualifiedName~ExampleTests.HomepageHasPlaywrightInTitle"   # single test
dotnet test --settings .runsettings -- Playwright.BrowserName=firefox Playwright.LaunchOptions.Headless=false # override settings
PWDEBUG=1 dotnet test --settings .runsettings     # Playwright Inspector
```

Browser OS dependencies need root (sudo resets PATH, so pass full paths):
`sudo env DOTNET_ROOT=$HOME/.dotnet $HOME/.dotnet/tools/pwsh PlayWrightDemo.Tests/bin/Debug/net10.0/playwright.ps1 install-deps chromium`

## Structure

- Test fixtures inherit from `PageTest` (from `Microsoft.Playwright.NUnit`), which provides a fresh `Page`/`Context` per test and the `Expect(...)` web-first assertions. Use `ContextTest` or `BrowserTest` when a test needs to manage contexts or pages itself.
- `.runsettings` at the repo root holds the browser, headless mode, and expect timeout. Pass it with `--settings` or the defaults apply.
- `NUnit.Framework` is a global using, set in the csproj.
