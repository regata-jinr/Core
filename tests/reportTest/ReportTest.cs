/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core;
using System.Threading.Tasks;
using Regata.Core.DataBase.Postgres.Context;
using System.Linq;
using System;
using System.IO;

namespace Tests
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public void ReportInfoTest()
        {
            Report.LogConnectionStringTarget = "MeasurementsLogConnectionString";
            Report.LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "ReportTest");
            Directory.CreateDirectory(Report.LogDir);
            Report.User = "bdrum";
            var msg = new Message()
            {
                Code     = 0,
                BaseBody = "TestInform BaseBody",
                Level    =  Status.Error,
                Place    = "InformLevelTest",
                Sender   = "ReportTest",
                Title    = "InformLevelTest",
                User     = "bdrum",
                TechBody = "TestInform TechBody"
            };

            Report.Notify(0);


            using (var lc = new LogContext("MeasurementsLogConnectionString"))
            {
                var last_log = lc.Logs.OrderBy(l => l.date_time).Last();

                Assert.AreEqual(msg.Level.ToString().ToUpper(), last_log.level);
                Assert.IsTrue(10 > (DateTime.Now.AddHours(-2) - last_log.date_time).TotalSeconds);
                Assert.AreEqual(msg.User, last_log.assistant);
                Assert.AreEqual(msg.Sender, last_log.frominstance);
            }
        }


    } // public class ReportTest
}     // namespace ReportTest
