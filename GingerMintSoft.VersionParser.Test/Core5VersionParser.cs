using System;
using System.Linq;
using GingerMintSoft.VersionParser.Architecture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = GingerMintSoft.VersionParser.Architecture.Version;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class Core5VersionParser
    {
        [TestMethod]
        public void FindCore5Arm64TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var downLoads = page.ReadVersions(Version.Core5, Sdk.Arm64);
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

            var downLoads = page.ReadVersions(Version.Core5, Sdk.Arm32);
            Assert.IsNotNull(downLoads);

            foreach (var downLoad in downLoads)
            {
                Console.WriteLine($"{downLoad} \r\n");
            }
        }
    }
}
