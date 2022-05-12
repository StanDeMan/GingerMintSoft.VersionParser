using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using GingerMintSoft.VersionParser.Architecture;
using GingerMintSoft.VersionParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Version = GingerMintSoft.VersionParser.Architecture.Version;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class SerializeDeserializeScrapingContent
    {
        [TestMethod]
        public void SerializeDeserializeScrapingContentTest()
        {
            var page = new HtmlPage("https://dotnet.microsoft.com");
            Assert.IsNotNull(page);

            page.BaseUri = "https://dotnet.microsoft.com";
            page.CultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            var uriDotNet = page.DotNetUri;
            Assert.IsNotNull(uriDotNet);

            var downloadUri = page.DownloadUri;
            Assert.AreEqual("download/dotnet", downloadUri);

            var catalog = new SdkScrapingCatalog
            {
                Culture = "en-Us",
                MicrosoftBaseUri = "https://dotnet.microsoft.com",
                ScraperList = new List<SdkScraper>()
                {
                   new()
                   {
                       Sdk = Sdk.Arm32,
                       Version = Version.Core3
                   },
                   new()
                   {
                       Sdk = Sdk.Arm64,
                       Version = Version.Core3
                   },
                   new()
                   {
                       Sdk = Sdk.Arm32,
                       Version = Version.Core6
                   },
                   new()
                   {
                       Sdk = Sdk.Arm64,
                       Version = Version.Core6
                   }
                }
            };

            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            var jsonCatalog = JsonConvert.SerializeObject(catalog, jsonSerializerSettings);
            Assert.IsNotNull(uriDotNet);

            var readCatalog = JsonConvert.DeserializeObject<SdkScrapingCatalog>(jsonCatalog, jsonSerializerSettings);
            Assert.IsNotNull(readCatalog);

            Console.WriteLine($"Culture: {readCatalog.Culture}");
            Console.WriteLine($"BaseUri: {readCatalog.MicrosoftBaseUri}\r\n");

            var readSdkScraperList = readCatalog.ScraperList ?? new List<SdkScraper>();

            foreach (var item in readSdkScraperList)
            {
                Console.WriteLine($"Sdk: {item.Sdk}, Version: {item.Version}");
            }

            File.WriteAllText(@"D:\path.txt", jsonCatalog);
        }
    }
}
