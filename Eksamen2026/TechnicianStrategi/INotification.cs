using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.TechnicianStrategi
{
    public interface INotification
    {
        public void NotifyTechnician(int sensorId);
    }
}