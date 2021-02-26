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
using Regata.Core.Messages;
using Regata.Core.DataBase.Postgres.Context;
using System.Linq;
using System;
using System.IO;

namespace Regata.Tests.Reports
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public void ReportInfoTest()
        {
            Report.LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "ReportTest");
            Directory.CreateDirectory(Report.LogDir);
            var msg = new Message(0)
            {

                  Caption       = "",
                  Head          = "",    
                  Code          = 0,        
                  Status        = Status.Error,      
                  Text          = "",
                  DetailedText  = "", 
                  Sender        = "ReportTest"

                //BaseBody = "TestInform BaseBody",
                //Level    =  Status.Error,
                //Place    = "InformLevelTest",
                //Sender   = "ReportTest",
                //Title    = "InformLevelTest",
                //User     = "bdrum",
                //TechBody = "TestInform TechBody"
            };

            Report.Notify(new Message(0), WriteToLog: true);

            using (var lc = new LogContext("RegataCoreLogCS"))
            {
                var last_log = lc.Logs.OrderBy(l => l.date_time).Last();

                Assert.AreEqual(msg.Status.ToString().ToUpper(), last_log.level);
                Assert.IsTrue(10 > (DateTime.Now.AddHours(-2) - last_log.date_time).TotalSeconds);
                Assert.AreEqual("bdrum", last_log.assistant);
                Assert.AreEqual(msg.Sender, last_log.frominstance);
            }
        }


    } // public class ReportTest
}     // namespace ReportTest
