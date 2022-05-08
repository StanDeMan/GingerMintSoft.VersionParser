using System;
using System.Linq;
using GingerMintSoft.VersionParser.Architecture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class Core5VersionParser
    {
        private const string  UriToCore5 = "https://dotnet.microsoft.com/en-us/download/dotnet/5.0";

        [TestMethod]
        public void FindCore5Arm64TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var downLoads = page.ReadVersions("5.0", Sdk.Arm64);
            Assert.IsNotNull(downLoads);

            foreach (var downLoad in downLoads)
            {
                Console.WriteLine($"{downLoad} \r\n");
            }
        }

        [TestMethod]
        public void FindCore5Arm32TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var downLoads = page.ReadVersions("5.0", Sdk.Arm32);
            Assert.IsNotNull(downLoads);

            foreach (var downLoad in downLoads)
            {
                Console.WriteLine($"{downLoad} \r\n");
            }
        }
    }
}
