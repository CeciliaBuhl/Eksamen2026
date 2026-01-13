using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Eksamen2026.VentilatorStrategi.Networkcommunication
{
    public class SocketClient
    {
        private readonly string _ip;
        private readonly int _port;

        public SocketClient(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        public void SendMessage(string message)
        {
            IPAddress ipAddress = IPAddress.Parse(_ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _port);

            using Socket client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                client.Connect(ipEndPoint);// forsøger at skabe kontakt til IP og port - ellers kastes fejl

                //Send besked
                var messageBytes = Encoding.UTF8.GetBytes(message);//omskriver string til bytes
                client.Send(messageBytes, SocketFlags.None);

                //Modtag ACK bekræftigelse fra serveren
                byte[] buffer = new byte[1024]; //midlertid lagerplads i bytes
                int received = client.Receive(buffer,SocketFlags.None);//læser bytes
                string response = Encoding.UTF8.GetString(buffer,0, received);//oversætter til string

                System.Console.WriteLine($"[Network] Server svarede {response}");

                client.Shutdown(SocketShutdown.Both);//lukker for både sender og modtager
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Network Error] Connection failed: {ex.Message}");
            }
        }
    }
}