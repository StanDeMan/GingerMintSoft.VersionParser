using GingerMintSoft.VersionParser.Architecture;
using Version = GingerMintSoft.VersionParser.Architecture.Version;

namespace GingerMintSoft.VersionParser.Models;

public class SdkScraper
{
    public Version Version { get; init; }

    public Sdk Family { get; init; }
}