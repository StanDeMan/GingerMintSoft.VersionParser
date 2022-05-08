using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class Core6VersionParser
    {
        private const string  UriToCore6 = "https://dotnet.microsoft.com/en-us/download/dotnet/6.0";

        [TestMethod]
        public void FindCore6Arm64TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var document = page.Load(UriToCore6);
            Assert.IsNotNull(document);

            var downLoads = document
                .DocumentNode
                .SelectNodes("//a[contains(text(), 'Arm64')]")
                .Select(x => x.GetAttributeValue("href", String.Empty))
                .ToList();

            for (var i = 0; i < downLoads.Count; i++)
            {
                if (downLoads[i].Contains("alpine") ||
                    downLoads[i].Contains("x32") ||
                    downLoads[i].Contains("x64") ||
                    downLoads[i].Contains("macos") ||
                    downLoads[i].Contains("windows") ||
                    downLoads[i].Contains("runtime") ||
                    downLoads[i].Contains("rc") ||
                    downLoads[i].Contains("preview"))
                {
                    downLoads.RemoveAt(i--);
                    continue;
                }

                Console.WriteLine($"{downLoads[i]} \r\n");
            }
        }

        [TestMethod]
        public void FindCore6Arm32TestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var document = page.Load(UriToCore6);
            Assert.IsNotNull(document);

            var downLoads = document
                .DocumentNode
                .SelectNodes("//a[contains(text(), 'Arm32')]")
                .Select(x => x.GetAttributeValue("href", String.Empty))
                .ToList();

            for (var i = 0; i < downLoads.Count; i++)
            {
                if (downLoads[i].Contains("alpine") ||
                    downLoads[i].Contains("x32") ||
                    downLoads[i].Contains("x64") ||
                    downLoads[i].Contains("macos") ||
                    downLoads[i].Contains("windows") ||
                    downLoads[i].Contains("runtime") ||
                    downLoads[i].Contains("rc") ||
                    downLoads[i].Contains("preview"))
                {
                    downLoads.RemoveAt(i--);
                    continue;
                }

                Console.WriteLine($"{downLoads[i]} \r\n");
            }
        }
    }
}