using System;
using System.Linq;
using GingerMintSoft.VersionParser.Architecture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = GingerMintSoft.VersionParser.Architecture.Version;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class ReadDownloadLinkAndChecksum
    {
        [TestMethod]
        public void ReadDownloadLink()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var sdkUri = "https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.202-linux-arm64-binaries";
            Console.WriteLine($"Download Uri: {sdkUri}\r\n");

            var (downLoadLink, checkSum) = page.ReadDownloadLinkAndChecksum($"{sdkUri}");
            Assert.IsNotNull(downLoadLink);
            Assert.IsNotNull(checkSum);

            Console.WriteLine($"Download Link: {downLoadLink} \r\n" +
                              $"Checksum: {checkSum} \r\n");

            sdkUri = "https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.418-linux-arm32-binaries";

            (downLoadLink, checkSum) = page.ReadDownloadLinkAndChecksum($"{sdkUri}");
            Assert.IsNotNull(downLoadLink);
            Assert.IsNotNull(checkSum);

            Console.WriteLine($"Download Link: {downLoadLink} \r\n" +
                              $"Checksum: {checkSum} \r\n");
        }
    }
}
