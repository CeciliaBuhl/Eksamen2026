using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eksamen2026.ProducerConsumer;
using Eksamen2026.VentilatorStrategi;

namespace Eksamen2026.GoF_Observer
{
    public class VentilatorObserver : IObserver
    {
        private readonly AirMonitorConsumer _airData;
        public IVentilator Ventilator {get;}//ventilator med id i ctor
        private VentilatorSpeed _currentSpeed;//enum
        private int _sensorId {get; set;}
        
        public VentilatorObserver(AirMonitorConsumer airData, IVentilator ventilator, int sensorId)
        {
            _airData = airData;
            Ventilator = ventilator;
            _sensorId = sensorId;
            airData.Attach(this);
        }
        public void Update()
        {
            if(_airData.CurrentSample != null && _airData.CurrentSample.SensorId == _sensorId)//nyeste data og at ventilatoren hører til den korrekte sensor
            {
                HandleVentilation(_airData.CurrentSample.Measurement);
            }
        }
        public void HandleVentilation(int measurement)
        {
            VentilatorSpeed speed;
            if(measurement<=800)//ventilation udfra CO2 niveau
            {
                speed = VentilatorSpeed.Off;
            }
            else if (measurement<=1000)
            {
                speed = VentilatorSpeed.Low;
            }
            else if(measurement<=1500)
            {
                speed = VentilatorSpeed.Medium;
            }
            else
            {
                speed = VentilatorSpeed.High;
            }

            if(speed != _currentSpeed)//opdater, hvis der ændres tilstand
            {
                _currentSpeed = speed;
                Ventilator.VentilatorSpeed(_currentSpeed, _sensorId);
            }
        }
    }
}