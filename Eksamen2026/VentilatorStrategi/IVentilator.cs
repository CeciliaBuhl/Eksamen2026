using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.VentilatorStrategi
{
    public interface IVentilator
    {
        public int VentilatorId {get;}
        public void VentilatorSpeed(VentilatorSpeed speed, int sensorId);
    }
}