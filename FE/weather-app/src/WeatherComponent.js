import React, { useState, useEffect } from 'react';
import axios from 'axios';

const WeatherComponent = () => {
  const [countries] = useState([
    { name: 'Indonesia', code : 'ID'},
    { name: 'United States', code: 'US' },
    { name: 'United Kingdom', code: 'GB' },
    { name: 'Japan', code: 'JP' },
    { name: 'France', code: 'FR' },
  ]);
  const [cities, setCities] = useState([]);
  const [selectedCountry, setSelectedCountry] = useState('ID');
  const [selectedCity, setSelectedCity] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);


  useEffect(() => {
    
    const countryCities = {
      ID: ['Jakarta', 'Bandung', 'Semarang', 'Surabaya', 'Makassar', 'Medan'],
      US: ['New York', 'Los Angeles', 'Chicago', 'Houston'],
      GB: ['London', 'Manchester', 'Birmingham'],
      JP: ['Tokyo', 'Osaka', 'Kyoto'],
      FR: ['Paris', 'Lyon', 'Marseille'],
    };

    setCities(countryCities[selectedCountry] || []);
    setSelectedCity(countryCities[selectedCountry] ? countryCities[selectedCountry][0] : ''); // Default city
  }, [selectedCountry]);


  useEffect(() => {
    if (!selectedCity) return;

    getWeatherData();
  }, [selectedCity]);

  const getWeatherData = () => {
    setIsLoading(true);
    setError(null);
    const url = `https://localhost:44321/WeatherForecast?city=${selectedCity}`;

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
