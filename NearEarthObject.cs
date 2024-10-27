using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearEarthObjectVisualization
{
    public class NearEarthObject
    {
        public string Name { get; set; }
        public double EstimatedDiameterMeters { get; set; }
        public double EstimatedDiameterFeet { get; set; }
        public double MissDistanceKm { get; set; }
        public double MissDistanceMi { get; set; }
        public double VelocityKmPerHour { get; set; }
        public double VelocityMiPerHour { get; set; }
        public bool IsPotentiallyDangerous { get; set; }
        public bool IsSentryObject { get; set; }
        public string OrbitingBody { get; set; }
    }
}
