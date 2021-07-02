# Base

This assemly contains main level of abstraction

- report system (logging and notification)
- database interaction
- managing of settings for application

The starting point of usage whole Core system is config file target.json.

It contains names of windows credential manager target that keep connection string for cloud and database.

# Settings

> This assembly aims to implementation of common settings mechanic for different apps.

Settings is a generic static class where input type represents an application settings.

In order to implement global setting such as language and verbosity level we provide abstract class ASettings. Argument type of generic class have to be derived from this one.

> **Global settings are read only and it synchronized with Current Settings. Don't create the same properties explicitly it will overwrite base behaviour and break synchronization.**

Entry point is specifying of assemply name:

~~~csharp

  public class DetectorSettings
    {
        public int       count_time  { get; set; }  = 10;
        public float     height      { get; set; } = 1;
        public string    name        { get; set; } = "D1";
        public bool      iswrite     { get; set; } = true;
        public TestEnum  iset5       { get; set; } = TestEnum.One;
        public List<int> iset6       { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
    }

    public class AppSettings : IEquatable<AppSettings>
    {
        public float            width                { get; set; } = 0.1f;
        public float            height               { get; set; } = 1;
        public string           name                 { get; set; } = "TestApp";
        public bool             showTitle            { get; set; } = true;
        public TestEnum         iset5                { get; set; } = TestEnum.One;
        public List<int>        iset6                { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
        public List<DetectorSettings> detectors      { get; set; } = new List<DetectorSettings>() { new DetectorSettings() };

// ...

Settings<AppSettings>.AssemblyName = "TestSettings";
~~~

By default all settings keeping in json file with Path "User\AppData\Roaming\Regata\AssemblyName":

~~~powershell
# bdrum at RUMLAB in ~\AppData\Roaming\Regata\TestSettings [18:11:51]
➜ cat .\settings.json
{
  "width": 0.1,
  "height": 1,
  "name": "DefStr",
  "showTitle": true,
  "iset5": "one",
  "iset6": [
    1,
    2,
    3,
    4,
    5
  ],
  "detectors": [
    {
      "count_time": 10,
      "height": 1,
      "name": "D1",
      "iswrite": true,
      "iset5": "one",
      "iset6": [
        1,
        2,
        3,
        4,
        5
      ]
    }
  ]
}
~~~

In order to get access to settings values use CurrentSettings field. It could be saved to the file.

~~~csharp
Settings<TestSettings>.AssemblyName = "TestSettings";
Settings<TestSettings>.CurrentSettings.height = 22f;
Settings<TestSettings>.Save();
~~~

Also use Load() method for update app settings.


# Report 

This class provides the way to combine all notifications into the one library.

Messaging of any level baised on the special code and could be shown with any GUI library.


## Codes

The main idea is usage of common way to all kind of reports.
To solve this we will use unique code for each system state.
Report could have few levels:

### Info 

Normal behavior like mail sent, user updated profile etc.
 
- [0-1000)  - Info codes
  - [  0- 99) - DataBase          
  - [100-199) - Cloud             
  - [200-299) - Detector          
  - [300-399) - Logger            
  - [400-499) - Settings          
  - [500-599) - Export:Excel      
  - [600-699) - Export:GoogleSheet
  - [700-799) - Export:CSV        

### Success

This class of status codes indicates the action requested by the client was received, understood, and accepted

- [1000-2000) - Success codes
   - [1000-1099) - DataBase           
   - [1100-1199) - Cloud              
   - [1200-1299) - Detector           
   - [1300-1399) - Logger             
   - [1400-1499) - Settings           
   - [1500-1599) - Export:Excel       
   - [1600-1699) - Export:GoogleSheet 
   - [1700-1799) - Export:CSV         

### Warning

Something unexpected; application will continue

- [2000-3000) - Warning codes
    - [2000-2099) - DataBase          
    - [2100-2199) - Cloud             
    - [2200-2299) - Detector          
    - [2300-2399) - Logger            
    - [2400-2499) - Settings          
    - [2500-2599) - Export:Excel      
    - [2600-2699) - Export:GoogleSheet
    - [2700-2799) - Export:CSV        

### Error

Something failed; application may or may not continue

- [3000-4000) - Error codes
   - [3000-3099) - DataBase          
   - [3100-3199) - Cloud             
   - [3200-3299) - Detector          
   - [3300-3399) - Logger            
   - [3400-3499) - Settings          
   - [3500-3599) - Export:Excel      
   - [3600-3699) - Export:GoogleSheet
   - [3700-3799) - Export:CSV        

## State handler

Each code is correspond to some state. Any state has one way to report via using 

~~~csharp
 Report.Notify(new Message(ushort code_number));
~~~

Such approach allows us to process events in common manner. Moreover it also allows to be free from localizations and naming.
Based on code developer can be free and add description of states in any language and verbosity based on code.

## Different UI wrappers

Report class contains event which called 'NotificationEvent' such event can be used for adding different UI wrappers of notification, e.g. winforms, wpf or any other.

## Logs

By default core Report libs has a setting of NLog logging service. It writes logs to file and to db.

## Email notification

Also user can add his email to list and recieve notification by email.


 


