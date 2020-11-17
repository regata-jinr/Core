using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regata.Core.Inform;
using System.Threading.Tasks;

namespace InformTest
{
    [TestClass]
    public class InformTest
    {
        [TestMethod]
        public async Task InformLevelTest()
        {
            Inform.Init("MeasurementsLogConnectionString", "RegataMail");
            await Inform.Info(new Message() { Code = 0, BaseBody = "TestInform BaseBody", Level=InformLevel.Info, Place = "InformLevelTest", Sender= "InformLevelTest method", Title = "InformLevelTest", User = "bdrum", TechBody = "" }, write2logs: true );
            }
    }
}
