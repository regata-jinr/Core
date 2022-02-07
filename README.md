# R E G A T A &nbsp;&nbsp; C O R E

Regata Core представляет собой платформу, унифицирующую работу программ нашего [сектора](http://regata.jinr.ru/).

## Введение

RegataCore имеет монолитную архитектуру и состоит 18 библиотек, некоторые из которых связаны между собой.

Основные потребности сектора - связь с базой данных для работы с информацией об образцах, стандартах, мониторах, облучениях и измерениях, а также работа с оборудованием.

Предполагалось, что программы будут доступны только через использование удаленного доступа на Windows Server. Таким образом вся архитектура платформы заточена на использование на сервере, что дает возможность опустить работу связанною с безопасностью:

- аутинтификация в БД (на сервере будет выполнятся через пользователя)
- передача адреса сервера БД

Кроме того, работа на сервере делает ненужной систему развертывания на основе GitHub actions программ-клиентов для конечных пользователей.

Программный стек на данный момент(02.2022):

- [.NET5](https://docs.microsoft.com/en-us/dotnet/core/introduction)
- [Microsoft Sql Server 2017 (Linux)](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-overview?view=sql-server-ver15)
- [EF Core](https://docs.microsoft.com/en-us/ef/core/)
- [MS Test](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [NuGet](https://www.nuget.org/)
- [Git](https://git-scm.com/)
- [GitHub](https://github.com/)
- [WinForms](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/?view=netdesktop-6.0)
- [NLog](https://nlog-project.org/)
- [CredentialManager](https://github.com/AdysTech/CredentialManager)

От двух последних позиций можно избавиться заменив их на входящие в состав .net библиотеки:
- NLog -> [ILogger](https://docs.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line)
- CredentialManager на [Microsoft.PowerShell.Security](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.security/?view=powershell-7.2)

Переход на собственный Windows Server также решает проблему доступа к данным. Сейчас мы используем вирутальную машину развернутую на [JinrCloud](https://cloud.jinr.ru/) с MS Sql Server 2017 for Linux, но после перехода на Windows Server, мы перейдем на MS Sql Server 2019. Так как сервер располагается в зале с экспериментальной установкой, соединение может быть настроено по локальной сети, что снимает проблему доступа к базе во время проведения эксперимента.

Всё разработанное программное обеспечение находится в облачном репозитории GitHub https://github.com/regata-jinr под лицензией [GPL 3.0](https://www.gnu.org/licenses/gpl-3.0.en.html).

Указанный программный стек и Windows Server выбран не просто так.
Мы имеем сильную зависимость в виде программ входящих в состав экспериментальной установки. Наши HPGE детекторы подключатся к ОС Windows. Также программа обработки спектров и библиотеки автоматизации доступны только под этой ОС.

## Сборка

Сборка программ может осуществляться как через Visual Studio, так и с помощью коммандной строки. Для упрощения сборки написан powershell скрипт [build](\build.ps1), который позволяет рекурсивно (добавление новых проектов не требует изменений в скрипте) производить:

- сборку всех или выбранного по именам проектов с указанной конфигурации
- тестирование выбранных проектов
- упаковку собираемых библиотек в nuget пакет, для последующего подключения к программам-клиентам и копирование пакетов в единую директорию packages\regata.core.[name]\version
- копирование файлов динамических библиотек в единую директорию, которую удобно использовать для ссылок: artifacts\Release\[Name]

Пример использования скрипта:

```powershell
.\build.ps1 -Release -IgnoreTest -Name base
Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  Base -> D:\GoogleDrive\Job\flnp\dev\regata\Core\artifacts\Release\Base\Base.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.29
```

## Добавление модулей в проект

Все модули имееют единое пространство имен, корневым пространством является пространство

```csharp
Regata.Core
```

Подключить модуль можно двумя способами:

- добавить в ручную ссылку на dll проекта в файл csproj, для этого я использовал переменную окружения REGATA_ARTIFACTS, которая содержит путь к директории с библиотеками и создается автоматически при запуски скрипта сборки.


```xml
  <ItemGroup>
        <Reference Include="base">
          <HintPath>$(REGATA_ARTIFACTS)\dlls\$(Configuration)\base\base.dll</HintPath>
        </Reference>
  </ItemGroup>
```

- используя пакетный менеджер nuget (предпочтительный способ, так как позволяет переключаться между версиями)

```powrshell
dotnet add .\MyProj.csproj package Regata.Core.Base  -s $env:REGATA_ARTIFACTS\packages\
```

## [Base](src/Base/README.md)

Предоставляет классы описывающие системы и компоненты, лежащие в основе всех используемых програм:

- [Codes](src/Base/Codes/README.md) используется для описания ошибок разных уровней. Каждой обрабатываемой и необрабатываемой ситуации ставится в соответствии код, обладающий определенным форматом, из которого можно определеить статус ситуации и ее номер, по которому можно найти место возникновение ситуации и её описание.

- [Messages](src/Base/Messages/README.md) содержит базовый класс для сообщения, который используется для логов и отображения через графический интерфейс пользователя(ГИП)

- [Report](src/Base/Report/README.md) библиотека оповещения о статусе программ. Позволяет вести журналирование, уведомлять пользователя о возникновении ситуаций через ГИП, а также электронную почту.

- [Settings](src/Base/Settings/README.md) библиотека настроек приложений. На основе этой библиотеки все необходимые настойки можно выделить в класс, который будет сериализирован при изменении параметров или десериализирован при загрузке приложения из определенного файла настроек.

- [Database](src/Base/Database/README.md) библиотека взаимодействия с базой данных. Мы используем [EF Core](https://docs.microsoft.com/en-us/ef/core/). Библиотека включается в себя модели описывающие наши данные, а также контекстный менеджер для обращения и работы с ними.

## [Cloud](src/Cloud/README.md)

Библиотека взаимодейтсвия с [облачным сервисом ОИЯИ](disk.jinr.ru), который используется для хранения спектров образцов и других файлов сектора.

## [CNF](src/CNF/readme.md)

Библиотека взаимодействия с файлами спектров (.cnf) позволяет читать и записывать данные в уже существующий спектр. Используется для программы просмотра информации в спектрах.


## [DataIO](src/DataIO/README.md)

Библиотека экспорта и импорта табличных данных.

Поддерживается импорт из GoogleSheet и CSV, планируется поддержка Excel.
  
## [Hardware](src/Hardware/README.md)

Библиотеки взаимодействия с оборудованием.

- [Detector](src/Hardware/Detector/README.md) библиотека взаимодействия с детектором HPGe Canberra.

- [SampleChanger](src/Hardware/SampleChanger/README.md) бибилотека взаимодействия с устройством смены образцов Xemo Systec

- [Scales](src/Hardware/Scales/README.md) библиотека взаимодействия с весами

## [UI Templates](src/UItemplates/README.md)

Содержит шаблоны интерфейсов. Палнируется перейти от winforms на [.NET Multi-platform App UI](https://docs.microsoft.com/en-us/dotnet/maui/what-is-maui).

- [WinForms](src/UItemplates/WinFormsTemplates/README.md) Библиотека содержащая шаблоны интерфейсов на WinForms.

