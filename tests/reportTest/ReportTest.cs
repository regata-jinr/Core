using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core.Report;
using System.Threading.Tasks;
using Regata.Core.DataBase.Postgres.Context;
using System.Linq;
using System;

namespace ReportTest
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public async Task ReportInfoTest()
        {
            Report.Init("MeasurementsLogConnectionString", "RegataMail");

            var msg = new Message()
            {
                Code     = 0,
                BaseBody = "TestInform BaseBody",
                Level    = InformLevel.Info,
                Place    = "InformLevelTest",
                Sender   = "InformLevelTest method",
                Title    = "InformLevelTest",
                User     = "bdrum",
                TechBody = "TestInform TechBody"
            };

            await Report.Info(msg, write2logs: true );


            using (var lc = new LogContext("MeasurementsLogConnectionString"))
            {
                var last_log = lc.Logs.OrderBy(l => l.date_time).Last();

                Assert.AreEqual(msg.Level.ToString().ToUpper(), last_log.level);
                Assert.IsTrue(10 > (DateTime.Now.AddHours(-2) - last_log.date_time).TotalSeconds);
                Assert.AreEqual(msg.User, last_log.assistant);
                Assert.AreEqual(msg.TechBody, last_log.message);
            }

        }
    }
}
