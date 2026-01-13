using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eksamen2026.ProducerConsumer;

namespace Eksamen2026.FilterStrategi
{
    public class HighestFilter : IFilter
    {
        public int ApplyFilter(List<AirSensorSampleData> airData)
        {
            if (airData == null || airData.Count == 0)//tjekker at airdata ikke er null
            {
                return 0;
            }
            return airData.Max(x => x.Measurement);//LINQ til at finde max
        }
    }
}