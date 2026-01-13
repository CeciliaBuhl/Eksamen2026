using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eksamen2026.ProducerConsumer;

namespace Eksamen2026.FilterStrategi
{
    public interface IFilter
    {
        public int ApplyFilter(List<AirSensorSampleData> airData);
    }
}