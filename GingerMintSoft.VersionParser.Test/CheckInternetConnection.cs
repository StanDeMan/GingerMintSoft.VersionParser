using System.Threading.Tasks;
using GingerMintSoft.VersionParser.Internet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class CheckInternetConnection
    {
        [TestMethod]
        public async Task CheckTest()
        {
            var ok = await Connection.CheckAsync();
            Assert.IsTrue(ok);
        }
    }
}
