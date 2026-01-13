using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eksamen2026.LoggerStrategi;
using Eksamen2026.ProducerConsumer;

namespace Eksamen2026.GoF_Observer
{
    public class AirQualityLogObserver : IObserver
    {
        private readonly AirMonitorConsumer _airData;
        public ILogger Logger { get; set; }
        public AirQualityLogObserver(AirMonitorConsumer airData, ILogger logger)
        {
            _airData = airData;
            Logger = logger;
            airData.Attach(this);
        }
        public void Update()
        {
            if (_airData.CurrentSample != null)//nyeste data
            {
                string message = $"{_airData.CurrentSample.TimeStamp} - PPM {_airData.CurrentSample.Measurement}, Sensor: {_airData.CurrentSample.SensorId}";
                Logger.Log(message);
            }
        }
    }
}