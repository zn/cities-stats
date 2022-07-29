using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SmallCities;

new YandexSearch(null).Test();

throw new Exception();

using var httpClient = new HttpClient();
object locker = new object();

var cities = Parser.GetCities(httpClient);
//var cities = getCitiesFromCsv();

var result = new List<CitySearchInfo>(cities.Count());

int itemsInChunk = cities.Count(); // 100;
int chunks = (int)Math.Ceiling(cities.Count() / (float)itemsInChunk);

var tasks = new Task[chunks];

for (int i = 0; i < chunks; i++)
{
    var region = cities.Skip(i * itemsInChunk).Take(itemsInChunk);
    tasks[i] = handleCitiesChunk(region);
}
Task.WaitAll(tasks);
Console.WriteLine("Done");


IReadOnlyList<CityRecord> getCitiesFromCsv()
{
    var config = new CsvConfiguration(CultureInfo.CurrentCulture)
    {
        DetectDelimiter = true,
        HasHeaderRecord = false
    };

    using (var streamReader = new StreamReader("cities.tsv"))
    {
        using(var csvReader = new CsvReader(streamReader, config))
        {
            return csvReader.GetRecords<CityRecord>().ToList();
        }
    }
}

async Task handleCitiesChunk(IEnumerable<CityRecord> cities)
{
    YandexSearch search = new YandexSearch(httpClient);
    foreach (var city in cities)
    {
        try
        {
            var serachResult = await search.SearchCity($"{city.City} {city.Region}");   
            lock(locker)
            {
                result.Add(serachResult);
            }    
        }
        catch(Exception ex)
        {
            Console.WriteLine($"[{city.City}] {ex.Message}");
        }

        await Task.Delay(400);
        
    }
    await Task.CompletedTask;
}

class CityRecord
{
    [Index(1)]
    public string City { get; set; }
    [Index(2)]
    public string Region { get; set; }
}