namespace OpenWeather.Core.Dto
{
    public class CityApiResponse
    {
        public CityApiResponse(string countryID, string name)
        {
            CountryID = countryID;
            Name = name;
        }
    
        public string CountryID { get; private set; }

        public string Name { get; private set; }
    }

    public class CountryApiResponse
    {
        public CountryApiResponse(string countryID, string name)
        {
            CountryID = countryID;
            Name = name;
        }

        public string CountryID { get; private set; }

        public string Name { get; private set; }
    }
}
