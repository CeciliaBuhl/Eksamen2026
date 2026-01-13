namespace VentilatorController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer();
            server.RunServer();
        }
    }
}