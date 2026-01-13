using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eksamen2026.ProducerConsumer;
using Eksamen2026.TechnicianStrategi;

namespace Eksamen2026.GoF_Observer
{
    public class TechnicianObserver : IObserver
    {
        private readonly AirMonitorConsumer _airData;
        public INotification Notification {get; set;}
        
        public TechnicianObserver(AirMonitorConsumer airData, INotification notification)
        {
            _airData = airData;
            Notification = notification;
            airData.Attach(this);
        }
        public void Update()
        {
            HandleNotification();
        }

        public void HandleNotification()
        {

            if (_airData.CurrentSample != null)//nyeste data
            {
                int measurement = _airData.CurrentSample.Measurement;//henter measurement fra CurrentSample
                if(measurement >2000)
                {
                    Notification.NotifyTechnician(_airData.CurrentSample.SensorId);
                }
            }
        }
    }
}