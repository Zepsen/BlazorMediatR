using MediatR;
using System;

namespace BlazorMediatr.Data
{
    public class WeaterForecastRequest : IRequest<WeatherForecastResponse[]>
    {
        public DateTime Date { get; set; }
    }

    public class WeatherForecastResponse
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
