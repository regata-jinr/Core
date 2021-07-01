# R E G A T A &nbsp;&nbsp; C O R E

Regata Core is the software framework of our experiment.
It contains more than 20 DLLs and represents our basics activities.

- [R E G A T A &nbsp;&nbsp; C O R E](#r-e-g-a-t-a--c-o-r-e)
  - [Base](#base)
    - [Codes](#codes)
    - [Messages](#messages)
    - [Report](#report)
    - [Settings](#settings)
  - [Cloud](#cloud)
  - [Cnf](#cnf)
  - [Database](#database)
    - [MS-SQL](#ms-sql)
    - [POSTGRES](#postgres)
  - [Export](#export)
    - [Excel](#excel)
    - [CSV](#csv)
    - [GoogleSheets](#googlesheets)
  - [Hardware](#hardware)
    - [Detector](#detector)
    - [SampleChanger](#samplechanger)
    - [Scales](#scales)
  - [UI Templates](#ui-templates)
    - [WinForms](#winforms)
  - [Thoughts about future of the Regata software](#thoughts-about-future-of-the-regata-software)

## [Base](https://github.com/regata-jinr/Core/tree/master/src/base)

### [Codes](https://github.com/regata-jinr/Core/tree/master/src/base#report)

### [Messages](https://github.com/regata-jinr/Core/tree/master/src/base#report)

### [Report](https://github.com/regata-jinr/Core/tree/master/src/base#report)

### [Settings](https://github.com/regata-jinr/Core/tree/master/src/base#settings)

## [Cloud](https://github.com/regata-jinr/Core/tree/master/src/cloud)

## [Cnf](https://github.com/regata-jinr/Core/tree/master/src/cnf)

## [Database](https://github.com/regata-jinr/Core/tree/master/src/database)

### [MS-SQL](https://github.com/regata-jinr/Core/tree/master/src/database/mssql)

### [POSTGRES](https://github.com/regata-jinr/Core/tree/master/src/database/postgres)

## [Export](https://github.com/regata-jinr/Core/tree/master/src/export)
  
### [Excel](https://github.com/regata-jinr/Core/tree/master/src/export/csv)
  
### [CSV](https://github.com/regata-jinr/Core/tree/master/src/export/excel)

### [GoogleSheets](https://github.com/regata-jinr/Core/tree/master/src/export/google_sheets)

## [Hardware](https://github.com/regata-jinr/Core/tree/master/src/hardware)

### [Detector](https://github.com/regata-jinr/Core/tree/master/src/hardware/Detector)

### [SampleChanger](https://github.com/regata-jinr/Core/tree/master/src/hardware/SampleChanger)

### [Scales](https://github.com/regata-jinr/Core/tree/master/src/hardware/Scales)

## [UI Templates](https://github.com/regata-jinr/Core/tree/master/src/ui_templates)

### [WinForms](https://github.com/regata-jinr/Core/tree/master/src/ui_templates/WinFormsTemplates)

## Thoughts about future of the Regata software

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
