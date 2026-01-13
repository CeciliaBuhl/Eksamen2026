using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.VentilatorStrategi
{
    public class Ventilator : IVentilator
    {
        public int VentilatorId {get;}

        public Ventilator(int ventilatorId)
        {
            VentilatorId = ventilatorId;
        }
        public void VentilatorSpeed(VentilatorSpeed speed, int sensorId)
        {
            Console.WriteLine($"Ventilator: {VentilatorId} for sensor {sensorId} - speedstatus: {speed}");
        }
    }
}