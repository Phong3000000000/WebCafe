using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;



namespace DOAN_CLOUND.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ActionResult> GetWeather(double latitude, double longitude)
        {
            string latitude_str = latitude.ToString();
            string longitude_str = longitude.ToString();
            latitude_str = latitude_str.Replace(',', '.');
            longitude_str = longitude_str.Replace(',', '.');
            string baseUrl = "https://api.open-meteo.com/v1/forecast";
            string parameters = $"?latitude={latitude_str}&longitude={longitude_str}&hourly=temperature_2m,precipitation_probability";
            string url = $"{baseUrl}{parameters}";


            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    // Trả dữ liệu về View
                    return View(weatherData);
                }
                else
                {
                    return Content("Không thể lấy dữ liệu thời tiết.");
                }
            }
            catch (Exception ex)
            {
                return Content($"Lỗi khi gọi API: {ex.Message}");
            }
        }
    }
}