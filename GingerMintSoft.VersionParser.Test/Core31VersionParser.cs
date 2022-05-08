using System;
using System.Linq;
using GingerMintSoft.VersionParser.Architecture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class Core31VersionParser
    {
        private const string  UriToCore6 = "https://dotnet.microsoft.com/en-us/download/dotnet/3.1";

        [TestMethod]
        public void FindCore31Arm64TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var downLoads = page.ReadVersions("3.1", Sdk.Arm64);
            Assert.IsNotNull(downLoads);

            foreach (var downLoad in downLoads)
            {
                Console.WriteLine($"{downLoad} \r\n");
            }
        }

        [TestMethod]
        public void FindCore31Arm32TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var downLoads = page.ReadVersions("3.1", Sdk.Arm32);
            Assert.IsNotNull(downLoads);

            foreach (var downLoad in downLoads)
            {
                Console.WriteLine($"{downLoad} \r\n");
            }
        }
    }
}
