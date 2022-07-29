// Replace "{query}" with the real query 
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

const string UrlFormat = "https://yandex.ru/search/?text={query}&numdoc=30";
const string OutputDirectory = "pages";

if(!Directory.Exists(OutputDirectory))
{
    Directory.CreateDirectory(OutputDirectory);
}

var citiesList = getCitiesFromCsv();

var httpclient = new HttpClient();
var random = new Random();

int counter = new DirectoryInfo(OutputDirectory).GetFiles().Count();
int startFrom = counter;
bool captcha = false;
int captchaAttempts = 0;

// add user-agent

for (int i = startFrom; i < citiesList.Count; i++)
{
    if(captcha)
    {
        Console.WriteLine("Waiting for 30 seconds to avoid captcha...");
        captchaAttempts++;
        Thread.Sleep(30000);
    }
    string cityAndRegion = citiesList[i].City + " " + citiesList[i].Region;
    Console.WriteLine($"Started working on: {counter}. {cityAndRegion}");

    string searchUrl = UrlFormat.Replace("{query}", cityAndRegion.Replace(" ", "+"));
    using var response = httpclient.GetAsync(searchUrl).Result;
    if(response.IsSuccessStatusCode)
    {
        Console.Write("Successful request. Reading the data... ");
        string data = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Done! The length is " + data.Length);

        if(data.Length < 15000)
        {
            if(captchaAttempts == 5)
            {
                throw new Exception("Couldn't solve captcha");
            }
            Console.WriteLine($"Captcha required.");
            captcha = true;
            i--;
            continue;
        }
        captchaAttempts = 0;
        captcha = false;
        string filename = OutputDirectory + "/" + cityAndRegion + ".html";
        Console.Write("Saving to the file " + filename + " ... ");
        File.WriteAllText(filename, data);
        Console.WriteLine("Done!");
    }
    else
        Console.WriteLine($"An error occured while trying search city: {cityAndRegion}. [{response.StatusCode}]");

    counter++;
    
    int msToSleep = random.Next(4000, 8000);
    Console.WriteLine($"Sleeping {msToSleep/1000} sec...");
    Thread.Sleep(msToSleep);
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
        using(var csvReader = new CsvReader(streamReader, config))
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