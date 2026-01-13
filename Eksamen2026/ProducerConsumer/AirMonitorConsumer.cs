using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Eksamen2026.FilterStrategi;
using Eksamen2026.GoF_Observer;

namespace Eksamen2026.ProducerConsumer
{
    public class AirMonitorConsumer : Subject
    {
        private readonly BlockingCollection<AirSensorSampleData> _dataQueue;
        private bool _isPaused = false;
        public AirSensorSampleData? CurrentSample { get; private set; }//seneste sample, så observeren kan hente den - kan være null
        private Dictionary<int, AirSensorSampleData> _sensorData;//Dictionary til filter
        
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
                        CurrentSample = sample;//opdater tilstand til observer 
                        Notify();//giv besked til observer
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