using HtmlAgilityPack;

namespace GingerMintSoft.VersionParser
{
    public class HtmlPage
    {
        private HtmlWeb Web { get; set; }

        public HtmlDocument? Document { get; set; }

        public HtmlPage()
        {
            Web = new HtmlWeb();
        }

        public HtmlDocument? Load(string htmlPage)
        {
            Document = Web.Load(htmlPage);

            return Document;
        }
    }
}