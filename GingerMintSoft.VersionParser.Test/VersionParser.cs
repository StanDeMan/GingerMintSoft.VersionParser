using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerMintSoft.VersionParser.Test
{
    [TestClass]
    public class VersionParser
    {
        [TestMethod]
        public void LoadPageTestMethod()
        {
            var page = new HtmlPage();
            Assert.IsNotNull(page);

            var content = page.Load("https://dotnet.microsoft.com/en-us/download/dotnet/6.0");
            Assert.IsNotNull(content);


        }
    }
}