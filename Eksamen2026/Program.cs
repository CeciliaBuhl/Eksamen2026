using System.Collections.Concurrent;
using Eksamen2026.GoF_Observer;
using Eksamen2026.ProducerConsumer;

BlockingCollection<AirSensorSampleData> sharedQueue = new BlockingCollection<AirSensorSampleData>();//Opret én tråd

var producer1 = new AirSensorProducer(1, sharedQueue);
var producer2 = new AirSensorProducer(2, sharedQueue);
var consumer = new AirMonitorConsumer(sharedQueue);

//Observer
AirQualityLogObserver airQualityLogObserver= new AirQualityLogObserver(consumer);

Console.WriteLine("Air monitor system started");
Thread producerThread1 = new Thread(producer1.StartProducing);
Thread producerThread2 = new Thread(producer2.StartProducing);
Thread consumerThread = new Thread(consumer.StartConsuming);

producerThread1.Start();
producerThread2.Start();
consumerThread.Start();

Console.WriteLine("Press 'p' to pause system");
Console.WriteLine("Press 'r' to resume system");
Console.WriteLine("Press 'x' to close system");
Console.ReadKey();

while (true)
{
    var consoleKeyInfo = Console.ReadKey();
    switch (consoleKeyInfo.KeyChar)
    {
        case 'p':
        case 'P':
            Console.WriteLine("\nPausing system");
            consumer.PauseConsuming();
            break;
        case 'r':
        case 'R':
            Console.WriteLine("\nResuming system");
            consumer.ResumeConsuming();
            break;
        case 'x':
        case 'X':
            Console.WriteLine("\nStopping system");
            producer1.StopProducing(); //stopper tråde
            producer2.StopProducing();

            producerThread1.Join();
            producerThread2.Join();

            sharedQueue.CompleteAdding();//lukker køen
            consumerThread.Join();//venter på køen bliverhelt tom og lukker monitor pænt
            Console.WriteLine("System stopped");
            return;
    }
}