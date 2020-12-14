# Settings

> This assembly aims to implementation of common mechanic for settings of different apps.

Settings is a generic static class where specified type represents an application settings.

The class provided common mechanics for settings from an app.

Entry point is specifying an settings class and assemply name:

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


