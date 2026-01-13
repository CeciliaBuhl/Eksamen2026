using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.LoggerStrategi
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }
        public void Log(string message)
        {
            try
            {
                // AppendAllText åbner, skriver og lukker filen med det samme
                File.AppendAllText(_filePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[File Error] Kunne ikke skrive til log: {ex.Message}");
            }
        }
    }
}