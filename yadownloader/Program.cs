// Replace "{query}" with the real query 
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


const string UrlFormat = "https://yandex.ru/search/?text={query}&numdoc=30";
const string OutputDirectory = "pages";

if (!Directory.Exists(OutputDirectory))
{
    Directory.CreateDirectory(OutputDirectory);
}

var citiesList = getCitiesFromCsv();

int counter = new DirectoryInfo(OutputDirectory).GetFiles().Count();
int startFrom = counter;

IWebDriver driver = new FirefoxDriver();

for (int i = startFrom; i < citiesList.Count; i++)
{
    string cityAndRegion = citiesList[i].City + " " + citiesList[i].Region;
    string searchUrl = UrlFormat.Replace("{query}", cityAndRegion.Replace(" ", "+"));
    driver.Url = searchUrl;

    ConsoleKeyInfo key;
    do
    {
        Console.WriteLine("Press any key...");
        key = Console.ReadKey();
    } while (key.Key != ConsoleKey.D);

    string html = driver.PageSource;

    string filename = OutputDirectory + "/" + cityAndRegion + ".html";
    Console.Write("Saving to the file " + filename + " ... ");
    File.WriteAllText(filename, html);

    Console.WriteLine("Done!");
    counter++;
}

IReadOnlyList<CityRecord> getCitiesFromCsv()
{
    var config = new CsvConfiguration(CultureInfo.CurrentCulture)
    {
        DetectDelimiter = true,
        HasHeaderRecord = false
    };

    using (var streamReader = new StreamReader("cities.tsv"))
    {
        using (var csvReader = new CsvReader(streamReader, config))
        {
            return csvReader.GetRecords<CityRecord>().ToList();
        }
    }
}


class CityRecord
{
    [Index(1)]
    public string City { get; set; }
    [Index(2)]
    public string Region { get; set; }
}