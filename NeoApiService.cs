using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NearEarthObjectVisualization
{
    public  class NeoApiService
    {
        private readonly string apiKey;

        public NeoApiService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<List<NearEarthObject>> FetchNeoDataAsync(string startDate, string endDate)
        {
            string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key=API_KEY";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JObject.Parse(response);

                List<NearEarthObject> neos = new List<NearEarthObject>();

                foreach (var day in data["near_earth_objects"])
                {
                    foreach (var neo in day)
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
                            OrbitingBody = (string)neo["close_approach_data"]["orbiting_body"]
                        });
                    }
                }

                return neos;
            }
        }
    }
}
