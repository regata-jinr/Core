# R E G A T A &nbsp;&nbsp; C O R E

We have few applications that uses unify functional such as:

* Data representation via tables that means editing, adding, exporting, view
* Common scenarios for Settings: keeping parameters in files
* Logger
* Notification

This repo is attempt to prepare templates that will make development process dramatically easier.

- [R E G A T A &nbsp;&nbsp; C O R E](#r-e-g-a-t-a--c-o-r-e)
  - [Cloud](#cloud)
  - [Cnf](#cnf)
  - [Database](#database)
  - [Export](#export)
  - [Hardware](#hardware)
  - [Logger](#logger)
  - [Models](#models)
  - [Notifications](#notifications)
  - [Settings](#settings)
  - [Tests](#tests)
  - [UI Templates](#ui-templates)
  - [Updater](#updater)
  - [Some thoughts about future of regata software](#some-thoughts-about-future-of-regata-software)

## Cloud

## Cnf

## Database

## Export

## Hardware

## Logger

## Models

## Notifications

## Settings

## Tests

## UI Templates

## Updater

## Some thoughts about future of regata software

Many of needed function already implemented in differences frameworks.

> **Looks like [WinUI](https://docs.microsoft.com/en-us/windows/apps/winui/) is a future of desktop development for windows**
> ![img](https://docs.microsoft.com/en-us/windows/apps/images/platforms-winui3.png)

E.g. UWP  provides a [guidelines for app settings](https://docs.microsoft.com/en-us/windows/uwp/design/app-settings/guidelines-for-app-settings)

It means that I can't avoid parsing settings file and view it as separate forms because it available by default.

The first question is what framework should I use for development. Here is some limitations that I have to keep in mind:

- We strongly depend from GENIE2K packages. That means users should have windows only for working.
- Some of additional functionality (e.g. [CalcConc](https://github.com/regata-jinr/CalcConc)) should be local, i.e. support working without internet connection. I think this is the reason why we should stay on desktop application instead of web because I want to use one framework for all apps.
- Some of users still using windows 7, but I believe that we should not be freeze by this and update all of the system to latest, or support windows 7 also. Here is some problem in case of we will use [MSIX](https://docs.microsoft.com/en-us/windows/msix/overview) for deployment.

I think the best solution for us is **[UWP](https://docs.microsoft.com/en-us/windows/uwp/get-started/universal-application-platform-guide)**.

As I can see [Microsoft.Terminal](https://github.com/microsoft/terminal/search?q=xaml) use XAML as GUI language and distributed via few channel including github releases, that priority channel for us.

Here is also some useful info that we will use in future versions of our applications about:

- [Versioning](https://github.com/dotnet/Nerdbank.GitVersioning)
- [Deployment](https://github.com/microsoft/github-actions-for-desktop-apps)
- [Processing response from github about latest release](https://github.com/NickeManarin/ScreenToGif/blob/9952ae7f833fe49d1f409edcc70953de26799ec6/ScreenToGif/Model/ApplicationViewModel.cs#L818)
