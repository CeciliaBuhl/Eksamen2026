using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Eksamen2026.ProducerConsumer
{
    public class AirMonitorConsumer
    {
        private readonly BlockingCollection<AirSensorSampleData> _dataQueue;
        private bool _isPaused = false;
        public AirMonitorConsumer(BlockingCollection<AirSensorSampleData> dataQueue)
        {
            _dataQueue = dataQueue;
        }

        public void StartConsuming()
        {
            while (!_dataQueue.IsCompleted)
            {
                try
                {
                    AirSensorSampleData sample = _dataQueue.Take();//tømmer kø lige meget hvad
                    if (!_isPaused)//udskriver kun, hvis den ikke er pauset
                    {
                        Console.WriteLine($"{sample.TimeStamp} - PPM {sample.Measurement}, Sensor: {sample.SensorId}");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            Console.WriteLine("No more data expected");
        }

        public void PauseConsuming()
        {
            _isPaused = true;
        }

        public void ResumeConsuming()
        {
            _isPaused = false;
        }

    }
}