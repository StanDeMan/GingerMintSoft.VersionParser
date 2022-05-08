using System.Runtime.Serialization;
using GingerMintSoft.VersionParser.Architecture;
using GingerMintSoft.VersionParser.Extensions;
using HtmlAgilityPack;
using Version = GingerMintSoft.VersionParser.Architecture.Version;

namespace GingerMintSoft.VersionParser
{
    public class HtmlPage
    {
        private HtmlWeb Web { get; set; }

        private HtmlDocument? Document { get; set; }

        public string MicrosoftBaseUri { get; set; } = "https://dotnet.microsoft.com";
        public string MicrosoftDotNetUri { get; set; } = "https://dotnet.microsoft.com/en-us/download/dotnet";

        public HtmlPage()
        {
            Web = new HtmlWeb();
        }

        private HtmlDocument? Load(string htmlPage)
        {
            Document = Web.Load(htmlPage);

            return Document;
        }

        public List<string> ReadDownloadPages(Version version, Sdk architecture)
        {
            var sdk = architecture == Sdk.Arm32 
                ? Sdk.Arm32.GetAttributeOfType<EnumMemberAttribute>()?.Value 
                : Sdk.Arm64.GetAttributeOfType<EnumMemberAttribute>()?.Value;

            var actual = version.GetAttributeOfType<EnumMemberAttribute>()?.Value;
            var htmlPage = new HtmlPage().Load($"{MicrosoftDotNetUri}/{actual}");

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
    }
}