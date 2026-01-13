using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace VentilatorController
{
    public class SocketServer
    {
    private bool _isRunning { get; set; } = true;
        public void RunServer()
        {
            //Opsætning for lytning af endpoint: Lytter på alle (Any) på port 2000
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 2000);

            //Laver ny socket - AdressFamily= IP adresse "InterNetwork", Stream = to-vejs data stream
            using Socket listener = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);//binder socket med local endpoint (ip og port)

            Console.WriteLine($"Listening on: {ipAddress}");
            listener.Listen();//starter lytning på socket

            Console.WriteLine($"[Server] Listening on port 2000...");

            while (_isRunning)
            {
                using Socket handler = listener.Accept();//pauser og venter på connection fra clienten, opret ny socket (handler) som kommunikere til specifik klient
                Console.WriteLine($"[Server]: Connected to client");

                byte[] buffer = new byte[1024]; //midlertidgig lagerplads i bytes
                int bytesReceived = handler.Receive(buffer, SocketFlags.None); //læser bytes
                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesReceived); //oversætter til string

                Console.WriteLine($"[Client]: {receivedData}");

                //ACK bekræftigelse tilbage til client
                string reply = "ACK";
                byte[] replyBytes = Encoding.UTF8.GetBytes(reply); //omskriver string til bytes
                handler.Send(replyBytes,SocketFlags.None);//sender bytes

                //Forbindelse lukkes af sig selv pga. using
            }
        }
    }
}