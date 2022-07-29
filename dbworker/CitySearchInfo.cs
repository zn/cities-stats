namespace SmallCities;

class CitySearchInfo
{
    public string Query { get; set; } // init maybe
    public DateTime Date { get; } = DateTime.Now;
    public List<SearchResultItem> SearchItems { get; set; }
}