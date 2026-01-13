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
        private Dictionary<int, AirSensorSampleData> _sensorData = new();//Dictionary til filter
        public IFilter Filter { get; private set; }
        private int _numberOfSensors = 3;
        public AirMonitorConsumer(BlockingCollection<AirSensorSampleData> dataQueue, IFilter filter)
        {
            _dataQueue = dataQueue;
            Filter = filter;
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
                        _sensorData[sample.SensorId] = sample;

                        if (_sensorData.Count == _numberOfSensors)//3 eller flere sensorer
                        {
                            var rawData = _sensorData.Values.ToList();
                            int filteredValue = Filter.ApplyFilter(rawData);//anvender filteret på rådata

                            sample.Measurement = filteredValue;
                            CurrentSample = sample;//opdater tilstand til observer

                            Console.WriteLine($"{Filter.GetType().Name} - Filtered: {filteredValue}");

                            Notify();//giv besked til observer
                        }
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