using System.Collections.Concurrent;
using Eksamen2026.Configuration;
using Eksamen2026.GoF_Observer;
using Eksamen2026.ProducerConsumer;
using Eksamen2026.TechnicianStrategi;
using Eksamen2026.VentilatorStrategi;

BlockingCollection<AirSensorSampleData> sharedQueue = new BlockingCollection<AirSensorSampleData>();//Opret én tråd

var producer1 = new AirSensorProducer(1, sharedQueue);
var producer2 = new AirSensorProducer(2, sharedQueue);
var producer3 = new AirSensorProducer(3, sharedQueue);
var consumer = new AirMonitorConsumer(sharedQueue);

//Strategi
INotification email = new EmailNotification();
INotification sms = new SMSNotification();

IVentilator ventilator1 = new Ventilator(1);
IVentilator ventilator2 = new Ventilator(2);
IVentilator ventilator3 = new Ventilator(3);

//Configuration
ConfigManager configManager = new ConfigManager();

VentilatorConfig officeCondig = new VentilatorConfig { OffSetting = 700, LowSetting = 900, MediumSetting = 1400, HighSetting = 1900 };
VentilatorConfig schoolConfig = new VentilatorConfig { OffSetting = 900, LowSetting = 1400, MediumSetting = 1900, HighSetting = 2400 };

configManager.SaveConfig("officeConfig.json", officeCondig);
configManager.SaveConfig("schoolConfig.json", schoolConfig);

var currentOfficeConfig = configManager.LoadConfig("officeConfig.json");
var currentSchoolConfig = configManager.LoadConfig("schoolConfig.json");

//Observers
AirQualityLogObserver airQualityLogObserver = new AirQualityLogObserver(consumer);
TechnicianObserver technicianObserver = new TechnicianObserver(consumer, email);

VentilatorObserver ventilatorObserver1 = new VentilatorObserver(consumer, ventilator1, 1);//uden config
VentilatorObserver ventilatorObserver2 = new VentilatorObserver(consumer, ventilator2, 2, currentSchoolConfig);
VentilatorObserver ventilatorObserver3 = new VentilatorObserver(consumer, ventilator3, 3, currentOfficeConfig);

var defaultConfig = new VentilatorConfig();//default værdier

Console.WriteLine("Air monitor system started");
Console.WriteLine($"Activated noticifation handler: {technicianObserver.Notification.GetType().Name}");
Console.WriteLine($"Loaded ventilator config {1}: Off: {defaultConfig.OffSetting} Low: {defaultConfig.LowSetting}, Medium: {defaultConfig.MediumSetting}, High: {defaultConfig.HighSetting}");
Console.WriteLine($"Loaded ventilator config {2}: Off: {currentSchoolConfig.OffSetting} Low: {currentSchoolConfig.LowSetting}, Medium: {currentSchoolConfig.MediumSetting}, High: {currentSchoolConfig.HighSetting}");
Console.WriteLine($"Loaded ventilator config {3}: Off: {currentOfficeConfig.OffSetting} Low: {currentOfficeConfig.LowSetting}, Medium: {currentOfficeConfig.MediumSetting}, High: {currentOfficeConfig.HighSetting}");

Thread producerThread1 = new Thread(producer1.StartProducing);
Thread producerThread2 = new Thread(producer2.StartProducing);
Thread producerThread3 = new Thread(producer3.StartProducing);
Thread consumerThread = new Thread(consumer.StartConsuming);

producerThread1.Start();
producerThread2.Start();
producerThread3.Start();
consumerThread.Start();

Console.WriteLine("Press 'e' to activate SMS notification");
Console.WriteLine("Press 's' to activate email notification");
Console.WriteLine("Press 'p' to pause system");
Console.WriteLine("Press 'r' to resume system");
Console.WriteLine("Press 'x' to close system");

while (true)
{
    var consoleKeyInfo = Console.ReadKey();
    switch (consoleKeyInfo.KeyChar)
    {
        case 'e':
        case 'E':
            Console.WriteLine("\nEmail notification activated");
            technicianObserver.Notification = new EmailNotification();
            break;
        case 's':
        case 'S':
            Console.WriteLine("\nSMS notification activated");
            technicianObserver.Notification = new SMSNotification();
            break;
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
            producer3.StopProducing();

            producerThread1.Join();
            producerThread2.Join();
            producerThread3.Join();

            sharedQueue.CompleteAdding();//lukker køen
            consumerThread.Join();//venter på køen bliverhelt tom og lukker monitor pænt
            Console.WriteLine("System stopped");
            return;
    }
}