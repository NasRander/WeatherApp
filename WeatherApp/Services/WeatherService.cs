using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherApp.Models;


namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "53a004c0b5ce58d4df65a0d60bb79c1f";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                if (ex.Message.Contains("404"))
                {
                    throw new Exception("City not found");
                }
                else
                {
                    throw new Exception("An error occurred while fetching the weather data");
                }
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);


            return new WeatherData
            {
                Description = json["weather"][0]["description"].ToString(),
                Temperature = (double)json["main"]["temp"],
                FeelsLike = (double)json["main"]["feels_like"],
                MinTemp = (double)json["main"]["temp_min"],
                MaxTemp = (double)json["main"]["temp_max"],
                Humidity = (int)json["main"]["humidity"],

            };
        }
    }
}
