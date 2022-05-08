using System;
using System.Linq;
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

            var document = page.Load(UriToCore5);
            Assert.IsNotNull(document);

            var downLoads = document
                .DocumentNode
                .SelectNodes("//a[contains(text(), 'Arm64')]")
                .Select(x => x.GetAttributeValue("href", string.Empty))
                .ToList();

            for (var i = 0; i < downLoads.Count; i++)
            {
                if (!downLoads[i].Contains("alpine") && !downLoads[i].Contains("x32") &&
                    !downLoads[i].Contains("x64") && !downLoads[i].Contains("macos") &&
                    !downLoads[i].Contains("windows") && !downLoads[i].Contains("runtime") &&
                    !downLoads[i].Contains("rc") && !downLoads[i].Contains("preview")) 
                    continue;

                downLoads.RemoveAt(i--);
                continue;
            }

            downLoads.Sort();
            downLoads.Reverse();

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

            var document = page.Load(UriToCore5);
            Assert.IsNotNull(document);

            var downLoads = document
                .DocumentNode
                .SelectNodes("//a[contains(text(), 'Arm32')]")
                .Select(x => x.GetAttributeValue("href", string.Empty))
                .ToList();

            for (var i = 0; i < downLoads.Count; i++)
            {
                if (!downLoads[i].Contains("alpine") && !downLoads[i].Contains("x32") &&
                    !downLoads[i].Contains("x64") && !downLoads[i].Contains("macos") &&
                    !downLoads[i].Contains("windows") && !downLoads[i].Contains("runtime") &&
                    !downLoads[i].Contains("rc") && !downLoads[i].Contains("preview")) 
                    continue;

                downLoads.RemoveAt(i--);
                continue;
            }

            downLoads.Sort();
            downLoads.Reverse();

            foreach (var downLoad in downLoads)
            {
                Console.WriteLine($"{downLoad} \r\n");
            }
        }
    }
}
