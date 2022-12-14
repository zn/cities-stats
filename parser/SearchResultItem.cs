namespace parser;

class SearchResultItem
{
    public int Id { get; set; }
    public int Position { get; set; }
    public string Url { get; set; }
    public string Domain { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public CitySearchInfo CitySearchInfoId { get; set; }
}