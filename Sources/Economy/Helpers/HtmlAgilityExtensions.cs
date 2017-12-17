using System.Linq;
using HtmlAgilityPack;

namespace Economy.Helpers
{
    public static class HtmlAgilityExtensions
    {
        public static HtmlDocument ToDocument(this string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }

        public static HtmlNode GetBody(this HtmlDocument document)
        {
            var body = document.DocumentNode.Descendants("body").Single();
            return body;
        }
    }
}
