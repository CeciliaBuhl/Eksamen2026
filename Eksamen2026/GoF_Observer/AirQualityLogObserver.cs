using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eksamen2026.ProducerConsumer;

namespace Eksamen2026.GoF_Observer
{
    public class AirQualityLogObserver : IObserver
    {
        private readonly AirMonitorConsumer _airData;
        public AirQualityLogObserver(AirMonitorConsumer airData)
        {
            _airData = airData;
            airData.Attach(this);
        }

        public void Update()
        {
            AirSensorSampleData? sample = _airData.CurrentSample;//seneste sample
            if (sample != null)
            {
                Console.WriteLine($"{sample.TimeStamp} - PPM {sample.Measurement}, Sensor: {sample.SensorId}");
            }
        }
    }
}