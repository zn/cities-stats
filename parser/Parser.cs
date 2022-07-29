using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace parser;

static class Parser
{
    const string sourceUrl = "https://ru.wikipedia.org/wiki/Список_городов_России_с_населением_менее_50_тысяч_жителей";

    public static CitySearchInfo ParseHtmlFile(string filePath)
    {
        return parseHtml(File.ReadAllText(filePath));
    }

    private static CitySearchInfo parseHtml(string html)
    {
        var result  = new CitySearchInfo();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        result.Query = doc.DocumentNode.SelectSingleNode("//meta[@property='og:title']")
                          .Attributes.First(x => x.Name == "content").Value;
        var resultBlocks = doc.DocumentNode.SelectNodes("//ul[@id='search-result']/li");

        result.SearchItems = new List<SearchResultItem>(resultBlocks.Count);

        int position = 1;
        foreach (var block in resultBlocks)
        {
            var aNode = block.SelectSingleNode(".//div[contains(@class, 'VanillaReact')]/a");
            if(aNode == null) // it happens when the link is by Yandex(yandex images, yandex translator). We don't care about it.
                continue;
            
            string linkTitle = aNode.InnerText.Trim();
            linkTitle = Regex.Replace(linkTitle, "\\s+", " ");

            string linkHref = aNode.Attributes.First(x => x.Name == "href").Value;

            var descriptionNode = block.SelectSingleNode(".//div[contains(@class, 'Organic-ContentWrapper')]");

            string description = descriptionNode.SelectSingleNode(".//div/span/label/span")?.InnerText;
            if(description == null)
            {
                description = descriptionNode.InnerText;
            }

            description = description.Replace("Читать ещё", "");
            description = Regex.Replace(description.Trim(), "\\s+", " ");

            result.SearchItems.Add(new SearchResultItem{
                Position = position,
                Title = linkTitle,
                Url = linkHref,
                Domain = new Uri(linkHref).Host,
                Description = description
            });

            position++;
        }

        return result;
    }
}