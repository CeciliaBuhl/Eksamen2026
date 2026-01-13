using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.VentilatorStrategi.Networkcommunication
{
    public class NetworkVentilator : IVentilator
    {
        private readonly SocketClient _client;
        public int VentilatorId {get;}

        public NetworkVentilator(SocketClient client, int ventilatorId)
        {
            _client = client;
            VentilatorId = ventilatorId;
        }
        public void VentilatorSpeed(VentilatorSpeed speed, int sensorId)
        {
            //send besked over netværket til styringssystem
            string message = $"Ventilator: {VentilatorId} - speedstatus: {speed}";
            _client.SendMessage(message);
        }
    }
}