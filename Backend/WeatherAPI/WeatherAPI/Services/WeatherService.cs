// Services/WeatherService.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherAPI.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WeatherAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "c50054a6b989e54064c99b7184d1eebd"; // Replace with your OpenWeatherMap API key

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var weatherData = ParseWeatherData(responseBody);
            weatherData.City = city;

            return weatherData;
        }

        private WeatherData ParseWeatherData(string responseBody)
        {
            var data = JObject.Parse(responseBody);
            var weatherData = new WeatherData
            {
                DailyForecasts = new List<DailyForecast>()
            };

            foreach (var item in data["list"])
            {
                var date = DateTime.Parse(item["dt_txt"].ToString());
                var forecast = new DailyForecast
                {
                    Date = date,
                    High = item["main"]["temp_max"].Value<double>(),
                    Low = item["main"]["temp_min"].Value<double>(),
                    Condition = item["weather"][0]["description"].ToString()
                };

                weatherData.DailyForecasts.Add(forecast);
            }

            var current = data["list"].First;
            weatherData.Temperature = current["main"]["temp"].Value<double>();
            weatherData.Humidity = current["main"]["humidity"].Value<double>();
            weatherData.WindSpeed = current["wind"]["speed"].Value<double>();
            weatherData.Condition = current["weather"][0]["description"].ToString();

            return weatherData;
        }
    }
}
