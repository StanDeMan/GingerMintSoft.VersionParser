namespace GingerMintSoft.VersionParser.Models
{
    public class SdkScrapingCatalog
    {
        public string? MicrosoftBaseUri { get; init; }

        public string? Culture { get; init; }

        public List<SdkScraper>? Sdks { get; init; } = new List<SdkScraper>();
    }
}
