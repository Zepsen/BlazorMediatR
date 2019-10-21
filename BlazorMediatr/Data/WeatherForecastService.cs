using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorMediatr.Data
{
    public class WeatherForecastService : IRequestHandler<WeaterForecastRequest, WeatherForecastResponse[]>
    {
        private readonly string _connectionString = "Data Source=EPUAKYIW3729;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<WeatherForecastResponse[]> Handle(WeaterForecastRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users;
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                users = await conn.QueryAsync<User>("spUsers_SelectAll", null, commandType: CommandType.StoredProcedure);
            }

            var rng = new Random();
            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecastResponse
            {
                Date = request.Date.AddDays(1),
                TemperatureC = rng.Next(-20, 55),
                Summary = users.FirstOrDefault().Name
                //Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
