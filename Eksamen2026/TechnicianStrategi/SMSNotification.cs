using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.TechnicianStrategi
{
    public class SMSNotification : INotification
    {
         public void NotifyTechnician(int sensorId)
        {
            Console.WriteLine($"[Error - Sensor. {sensorId}] - SMS sent to technician");
        }
    }
}