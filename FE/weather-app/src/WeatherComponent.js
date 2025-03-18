import React, { useState, useEffect } from 'react';
import axios from 'axios';

const WeatherComponent = () => {
 
  const [countries, setCountries] = useState([]);
  const [cities, setCities] = useState([]);
  const [selectedCountry, setSelectedCountry] = useState('');
  const [selectedCity, setSelectedCity] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const baseUrl = process.env.REACT_APP_BASE_API_URL;
 
  // Fetch countries from Countries API
  useEffect(() => {
    axios.get(`${baseUrl}/location/countries`)
      .then(response => {
         const countryList = response.data.data.map(country => ({
          name: country.name,
          code: country.countryID
        }));
        setCountries(countryList);
        setSelectedCountry(countryList[0]?.code || '');
      })
      .catch(error => console.error('Error fetching countries:', error));
  }, []);

  // Fetch cities when a country is selected
  useEffect(() => {
    if (!selectedCountry) return;

    const options = {
      method: 'GET',
      url: `${baseUrl}/location/cities?countryID=${selectedCountry}`
    };

    axios.request(options)
      .then(response => {
        console.log(response);
      
        const cityList = response.data.data.map(region => region.name);
        setCities(cityList);
        setSelectedCity(cityList[0] || '');
      })
      .catch(error => console.error('Error fetching cities:', error));
  }, [selectedCountry]);

  useEffect(() => {
    if (!selectedCity) return;

    getWeatherData();
  }, [selectedCity]);



  const getWeatherData = () => {
    setIsLoading(true);
    setError(null);
    const url = `${baseUrl}/WeatherForecast?city=${selectedCity}`;

    axios.get(url)
      .then(response => {
        setWeatherData(response.data);
        setIsLoading(false);
      })
      .catch(err => {
        setError('Failed to fetch weather data.');
        setIsLoading(false);
      });
  };

  return (
    <div className="weather-container">
      <h2>Weather Information</h2>

      {/* Country Selection */}
      <label htmlFor="countrySelect">Select Country:</label>
      <select
        id="countrySelect"
        value={selectedCountry}
        onChange={(e) => setSelectedCountry(e.target.value)}
      >
        {countries.map(country => (
          <option key={country.code} value={country.code}>
            {country.name}
          </option>
        ))}
      </select>

      {/* City Selection */}
      <label htmlFor="citySelect">Select City:</label>
      <select
        id="citySelect"
        value={selectedCity}
        onChange={(e) => setSelectedCity(e.target.value)}
        disabled={!cities.length}
      >
        {cities.map(city => (
          <option key={city} value={city}>
            {city}
          </option>
        ))}
      </select>

      {isLoading && <div>Loading weather data...</div>}

      {error && <div className="error">{error}</div>}

      {weatherData && (
        <div>
        <h3>Weather in {weatherData.data.name}</h3>
        <p>Temperature:  {weatherData.data.main.temp}°F/{weatherData.data.main.tempInCelcius}°C</p>
        <p>Weather: {weatherData.data.weather[0].description}</p>
        <p>Humidity: {weatherData.data.main.humidity} %</p>
        <p>Pressure: {weatherData.data.main.pressure}hPa</p>
        <p>Visibility: {weatherData.data.visibility}meter</p>
        <p>Cloudiness: {weatherData.data.clouds.all}%</p>
        <p>Wind: {weatherData.data.wind.speed}meter/sec</p>
      </div>
      )}
    </div>
  );
};

export default WeatherComponent;
