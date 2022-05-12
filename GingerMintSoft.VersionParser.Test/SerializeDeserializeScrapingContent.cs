using System.Collections.Generic;
using System.Globalization;
using GingerMintSoft.VersionParser.Architecture;
using GingerMintSoft.VersionParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

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
                   new SdkScraper()
                   {
                       Sdk = Sdk.Arm32,
                       Version = Version.Core3
                   },
                   new SdkScraper()
                   {
                       Sdk = Sdk.Arm64,
                       Version = Version.Core3
                   },
                   new SdkScraper()
                   {
                       Sdk = Sdk.Arm64,
                       Version = Version.Core6
                   },
                   new SdkScraper()
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

            var readCatalog = JsonConvert.DeserializeObject(jsonCatalog, jsonSerializerSettings);
            Assert.IsNotNull(readCatalog);
        }
    }
}
