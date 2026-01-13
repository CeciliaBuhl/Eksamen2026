using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eksamen2026.ProducerConsumer
{
    public class AirSensorProducer
    {
        private int _currentMeasurement = 1000;//Initialiseres til 1000 ppm

        private readonly Random _random = new Random();//field for at 2 sensorer ikke opretter samme random i GenerateSample()
        private readonly BlockingCollection<AirSensorSampleData> _dataQueue;
        public int SensorId { get; }//bruges i ctor til identificering
        private bool _isProducing = true;
        public AirSensorProducer(int sensorId, BlockingCollection<AirSensorSampleData> dataQueue)
        {
            SensorId = sensorId;
            _dataQueue = dataQueue;
        }
        public AirSensorSampleData GenerateSample()//returnere sampledata med alt nødvendigdata
        {
            //Producere heltal 1-10
            int randomSample = _random.Next(1, 11);
            if (randomSample < 6)
            {
                _currentMeasurement -= 50;//træk 50 ppm fra
            }
            else
            {
                _currentMeasurement += 50;//læg 50 ppm til
            }

            //Begrænse intervallet 500-3000 ppm
            if (_currentMeasurement < 500)
            {
                _currentMeasurement = 500;
            }
            if (_currentMeasurement > 3000)
            {
                _currentMeasurement = 3000;
            }

            AirSensorSampleData distanceSample = new AirSensorSampleData(measurement: _currentMeasurement, sensorId: SensorId, timeStamp: DateTime.Now);
            return distanceSample;
        }

        public void StartProducing()
        {
            while (_isProducing)
            {
                AirSensorSampleData newSample = GenerateSample();
                _dataQueue.Add(newSample);
                Thread.Sleep(10000);//opdatere hvert 10. sekund
            }
            _dataQueue.CompleteAdding();
            Console.WriteLine($"Sensor {SensorId} stopped.");
        }

        public void StopProducing()
        {
            _isProducing = false;
        }
    }
}