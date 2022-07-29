using System.Text.RegularExpressions;
using System.Xml.Serialization;
using HtmlAgilityPack;
using SearchXmlRoot = SmallCities.Serialization.Yandexsearch;

namespace SmallCities;

class YandexSearch
{
    // Replace "{query}" with the real query 
    // not working because Yandex doesn't give me limits for using api
    //const string urlFormat = "https://yandex.ru/search/xml?user=alexandersv1&key=03.1007612373:4140c4cefa71416697fada4c6354f27c&query={query}&l10n=ru&sortby=rlv&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D30.docs-in-group%3D1";
    const string urlFormat = "https://yandex.ru/search/?text={query}&numdoc=30";
    private readonly HttpClient _httpclient;

    public YandexSearch(HttpClient httpclient)
    {
        _httpclient = httpclient;
    }

    public async Task<CitySearchInfo> SearchCity(string cityAndRegion)
    {
        string searchUrl = urlFormat.Replace("{query}", cityAndRegion.Replace(" ", "+"));
        using var response = await _httpclient.GetAsync(searchUrl);
        
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception($"An error occured while trying search city: {cityAndRegion}. [{response.StatusCode}]");
        }

        string data = await response.Content.ReadAsStringAsync();
        var result = parseXml(data);
        result.Query = cityAndRegion;
        return result;
    }

    public void Test()
    {
        var result = parseHtml("");

        System.Console.WriteLine(result.Query);
        System.Console.WriteLine(result.Date.ToShortDateString());
        System.Console.WriteLine("Results:\n");
        foreach (var item in result.SearchItems)
        {
            System.Console.WriteLine($"{item.Position}. {item.Title}");
            System.Console.WriteLine(item.Url);
            System.Console.WriteLine(item.Description);
            System.Console.WriteLine();
        }
    }

    private CitySearchInfo parseHtml(string html)
    {
        var result  = new CitySearchInfo();

        html = File.ReadAllText("/home/alex/Downloads/ачинск красноярский край — Яндекс нашлось 6 млн результатов.html");
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
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
                Description = description
            });

            position++;
        }

        return result;
    }

    private CitySearchInfo parseXml(string xml)
    {
        SearchXmlRoot data;
        XmlSerializer serializer = new XmlSerializer(typeof(SearchXmlRoot));
        using (StringReader reader = new StringReader(xml))
        {
            data = (SearchXmlRoot)serializer.Deserialize(reader);
        }

        if(data.Response.Error != null)
        {
            throw new Exception($"{data.Response.Error.Text}. Code: {data.Response.Error.Code}");
        }

        var result = new CitySearchInfo();

        int itemsCount = data.Response.Results.Grouping.DocsInGroup;
        result.SearchItems = new List<SearchResultItem>(itemsCount);

        foreach (var item in data.Response.Results.Grouping.Group)
        {
            result.SearchItems.Add(new SearchResultItem
            {
                Position = item.Doccount,
                Url = item.Doc.Url,
                Domain = item.Doc.Domain,
                Title = item.Doc.Headline,
                Description = item.Doc.Text
            });
        }

        return result;
    }
}