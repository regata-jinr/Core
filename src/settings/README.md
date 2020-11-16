# Settings

> This assembly aims to implementation of common mechanic for settings of different apps.

Settings is a generic static class where specified type represents an application settings.

The class provided common mechanics for settings from an app.

Entry point is specifying an settings class and assemply name:

~~~csharp

 public class InternalSettings
    {
        public float     iset1 { get; set; }  = 0.1f;
        public int       iset2 { get; set; } = -1;
        public string    iset3 { get; set; } = "DefStr";
        public bool      iset4 { get; set; } = true;
        public TestEnum  iset5 { get; set; } = TestEnum.One;
        public List<int> iset6 { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
    }

    public class TestSettings : IEquatable<TestSettings>
    {
        public float            set1 { get; set; } = 0.1f;
        public int              set2 { get; set; } = -1;
        public string           set3 { get; set; } = "DefStr";
        public bool             set4 { get; set; } = true;
        public TestEnum         set5 { get; set; } = TestEnum.One;
        public List<int>        set6 { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
        public InternalSettings set7 { get; set; } = new InternalSettings();
    }

// ...

Settings<AppSettings>.AssemblyName = "TestSettings";
~~~

By default all settings keeping in json file with Path "User\AppData\Roaming\Regata\AssemblyName":

~~~powershell
# bdrum at RUMLAB in ~\AppData\Roaming\Regata\TestSettings [18:11:51]
➜ cat .\settings.json
{
  "set1": 0.1,
  "set2": -1,
  "set3": "DefStr",
  "set4": true,
  "set5": "one",
  "set6": [
    1,
    2,
    3,
    4,
    5
  ],
  "set7": {
    "iset1": 0.1,
    "iset2": -1,
    "iset3": "DefStr",
    "iset4": true,
    "iset5": "one",
    "iset6": [
      1,
      2,
      3,
      4,
      5
    ]
  }
}
~~~

To give access to settings values use CurrentSettings field. It could be saved to the file.

~~~csharp
Settings<TestSettings>.AssemblyName = "TestSettings";
Settings<TestSettings>.CurrentSettings.set2 = 22;
Settings<TestSettings>.Save();
~~~

Also use Load() method for update app settings. 


