using HtmlAgilityPack;

namespace GingerMintSoft.VersionParser
{
    public class HtmlPage
    {
        private HtmlWeb Web { get; set; }

        public HtmlDocument? Content { get; set; }

        public HtmlPage()
        {
            Web = new HtmlWeb();
        }

        public HtmlDocument? Load(string htmlPage)
        {
            Content = Web.Load(htmlPage);

            return Content;
        }
    }
}