using HtmlAgilityPack;
using System.Runtime.Serialization;
using GingerMintSoft.VersionParser.Architecture;
using GingerMintSoft.VersionParser.Extensions;
using Version = GingerMintSoft.VersionParser.Architecture.Version;

namespace GingerMintSoft.VersionParser
{
    public class HtmlPage
    {
        private HtmlWeb Web { get; }

        private HtmlDocument? Document { get; set; }

        public static string MicrosoftUri { get; set; } = "https://dotnet.microsoft.com";

        public string DotNetUri { get; set; } = $"{MicrosoftUri}/en-us/download/dotnet";

        /// <summary>
        /// Constructor 
        /// </summary>
        public HtmlPage()
        {
            Web = new HtmlWeb();
        }

        /// <summary>
        /// Load Html page as content
        /// </summary>
        /// <param name="htmlPage">Load this page</param>
        /// <returns>Html document</returns>
        private HtmlDocument? Load(string htmlPage)
        {
            Document = Web.Load(htmlPage);

            return Document;
        }

        /// <summary>
        /// Read download .NET versions at given page
        /// </summary>
        /// <param name="version">Search for this .NET version</param>
        /// <param name="architecture">Search for this architecture/bitness</param>
        /// <returns>List of partially version download uris</returns>
        public List<string> ReadDownloadPages(Version version, Sdk architecture)
        {
            var sdk = architecture == Sdk.Arm32 
                ? Sdk.Arm32.GetAttributeOfType<EnumMemberAttribute>()?.Value 
                : Sdk.Arm64.GetAttributeOfType<EnumMemberAttribute>()?.Value;

            var actual = version.GetAttributeOfType<EnumMemberAttribute>()?.Value;
            var htmlPage = new HtmlPage().Load($"{DotNetUri}/{actual}");

            var downLoads = htmlPage?.DocumentNode
                .SelectNodes($"//a[contains(text(), '{sdk}')]")
                .Select(x => x.GetAttributeValue("href", string.Empty))
                .ToList();

            for (var i = 0; i < downLoads?.Count; i++)
            {
                if (!downLoads[i].Contains("alpine") && !downLoads[i].Contains("x32") &&
                    !downLoads[i].Contains("x64") && !downLoads[i].Contains("macos") &&
                    !downLoads[i].Contains("windows") && !downLoads[i].Contains("runtime") &&
                    !downLoads[i].Contains("rc") && !downLoads[i].Contains("preview")) 
                    continue;

                downLoads.RemoveAt(i--);
            }

            downLoads?.Sort();
            downLoads?.Reverse();

            return downLoads ?? new List<string>();
        }

        /// <summary>
        /// Read actual download partial uri for .NET version and bitness
        /// </summary>
        /// <param name="version">Search for this .NET version</param>
        /// <param name="architecture">Search for this architecture/bitness</param>
        /// <returns>Partial version download uri</returns>
        public string ReadActualDownloadPage(Version version, Sdk architecture)
        {
            return ReadDownloadPages(version, architecture).First();
        }

        /// <summary>
        /// Read download partial uri for .NET version, bitness and a specific SDK version
        /// </summary>
        /// <param name="version">Search for this .NET version</param>
        /// <param name="specificVersion">Search for this .NET SDK version</param>
        /// <param name="architecture">Search for this architecture/bitness</param>
        /// <returns>Partial version download uri</returns>
        public string ReadDownloadPageForVersion(Version version, string specificVersion, Sdk architecture)
        {
            return ReadDownloadPages(version, architecture).First(x => x.Contains(specificVersion));
        }

        /// <summary>
        /// Read .NET download link with related checksum
        /// </summary>
        /// <param name="uri">Uri to download SDK page</param>
        /// <returns>Download SDK uri and checksum</returns>
        public (string? downLoadLink, string? checkSum) ReadDownloadLinkAndChecksum(string uri)
        {
            var htmlPage = new HtmlPage().Load($"{uri}");

            // .NET SDK download link and checksum
            return 
                (htmlPage?.DocumentNode
                .SelectNodes("//a[@id='directLink']")
                .Select(x => x.GetAttributeValue("href", string.Empty))
                .First(), 
                htmlPage?.DocumentNode
                .SelectNodes("//input[@id='checksum']")
                .Select(x => x.GetAttributeValue("value", string.Empty))
                .First());
        }
    }
}