namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int Humidity { get; set; }
    }
}
