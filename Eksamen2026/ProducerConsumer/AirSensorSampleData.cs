using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.ProducerConsumer
{
    public class AirSensorSampleData
    {
        public int Measurement { get; set; }
        public int SensorId { get; set; }
        public DateTime TimeStamp{ get; set; }
        public AirSensorSampleData(int measurement, int sensorId, DateTime timeStamp)
        {
            Measurement = measurement;
            SensorId = sensorId;
            TimeStamp = timeStamp;
        }
    }
}