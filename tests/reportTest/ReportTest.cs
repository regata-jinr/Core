/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core;
using Regata.Core.Messages;
using Regata.Core.DataBase;
using System.Linq;
using System;

namespace Regata.Tests.Reports
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public void ReportInfoTest()
        {
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

            using (var lc = new RegataContext())
            {
                var last_log = lc.Logs.OrderBy(l => l.DateTime).Last();

                Assert.AreEqual(msg.Status.ToString().ToUpper(), last_log.Level);
                Assert.IsTrue(10 > (DateTime.Now.AddHours(-2) - last_log.DateTime).TotalSeconds);
                Assert.AreEqual("bdrum", last_log.Assistant);
                Assert.AreEqual(msg.Sender, last_log.Frominstance);
                Assert.AreEqual(msg.Code, last_log.Code);
            }
        }


    } // public class ReportTest
}     // namespace ReportTest
