using parser;

// Command to start the container:
//
// docker run --name cities -p 5432:5432 -e POSTGRES_PASSWORD=admin -v cities -d postgres



if(args.Length < 1)
{
    throw new Exception("Expected path to a directory with Yandex search pages\n Pass it: dotnet parser.dll /path/to/folder");
}

string sourceDirectory = args[0];

if(!Directory.Exists(sourceDirectory))
{
    throw new Exception("Directory not found");
}

var files = new DirectoryInfo(sourceDirectory).GetFiles("*.html");

var result = new List<CitySearchInfo>(files.Length);
using var dbContext = new AppDbContext();
foreach (var file in files)
{
    try
    {
        var queryInfo = Parser.ParseHtmlFile(file.FullName);
        result.Add(queryInfo);
        Console.WriteLine(file.Name + " - OK");
    }
    catch(Exception ex)
    {
        System.Console.WriteLine(file.Name + " - BAD");
        Console.WriteLine("\t"+ex);
    }
}
dbContext.AddRange(result);
dbContext.SaveChanges();
System.Console.WriteLine("Done!");