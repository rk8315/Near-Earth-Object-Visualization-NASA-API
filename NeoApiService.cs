using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NearEarthObjectVisualization
{
    public  class NeoApiService
    {
        private readonly string _apiKey;

        public NeoApiService()
        {
            _apiKey = Environment.GetEnvironmentVariable("NASA_NEO_API_KEY");
        }

        public async Task<List<NearEarthObject>> FetchNeoDataAsync(string startDate, string endDate)
        {
            string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JObject.Parse(response);

                List<NearEarthObject> neos = new List<NearEarthObject>();

                foreach (var day in ((JObject)data["near_earth_objects"]).Properties())
                {
                    foreach (var neo in day.Value)
                    {
                        neos.Add(new NearEarthObject
                        {
                            Name = (string)neo["name"],
                            EstimatedDiameterMeters = ((double)neo["estimated_diameter"]["meters"]["estimated_diameter_min"] + (double)neo["estimated_diameter"]["meters"]["estimated_diameter_max"]) / 2.0,
                            EstimatedDiameterFeet = ((double)neo["estimated_diameter"]["feet"]["estimated_diameter_min"] + (double)neo["estimated_diameter"]["feet"]["estimated_diameter_max"]) / 2.0,
                            MissDistanceKm = (double)neo["close_approach_data"][0]["miss_distance"]["kilometers"],
                            MissDistanceMi = (double)neo["close_approach_data"][0]["miss_distance"]["miles"],
                            VelocityKmPerHour = (double)neo["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"],
                            VelocityMiPerHour = (double)neo["close_approach_data"][0]["relative_velocity"]["miles_per_hour"],
                            IsPotentiallyDangerous = (bool)neo["is_potentially_hazardous_asteroid"],
                            IsSentryObject = (bool)neo["is_sentry_object"],
                            OrbitingBody = (string)neo["close_approach_data"][0]["orbiting_body"]
                        });
                    }
                }

                return neos;
            }
        }
    }
}
