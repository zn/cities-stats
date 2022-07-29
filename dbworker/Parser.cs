using HtmlAgilityPack;

namespace SmallCities;


// parser Wikipedia
static class Parser
{
    const string sourceUrl = "https://ru.wikipedia.org/wiki/Список_городов_России_с_населением_менее_50_тысяч_жителей";
    
    // Format: { "City": "Region" }
    // It will be about 800 items
    public static IEnumerable<CityRecord> GetCities(HttpClient httpClient)
    {
        // TODO: replace with a real parser

        return new List<CityRecord>
        {
            new CityRecord{ City = "Зима", Region = "Иркутская область"},
            //{"Саянск", "Иркутская область"},
            //{"Тулун", "Иркутская область"},
            //{"Нижнеудинск", "Иркутская область"},
            //{"Тайшет", "Иркутская область"},
        };
    }
}