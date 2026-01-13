using Eksamen2026.FilterStrategi;
using Eksamen2026.ProducerConsumer;
using NUnit.Framework;

namespace Test;

[TestFixture]
public class Tests
{
    private HighestFilter _highestFilter;
    [SetUp]
    public void Setup()
    {
        _highestFilter = new HighestFilter();
    }

    [Test]
    [TestCase(800, 800, 800, 800)]   // Alle er ens
    [TestCase(800, 2000, 1200, 2000)] // Forskellige - midterste er højest
    [TestCase(2000, 2000, 1200, 2000)] // To ens højeste
    public void ReturnsCorrectMax(int m1, int m2, int m3, int expected)
    {
        var data = new List<AirSensorSampleData>
        {
            new AirSensorSampleData(m1, 1, DateTime.Now),
            new AirSensorSampleData(m2, 2, DateTime.Now),
            new AirSensorSampleData(m3, 3, DateTime.Now)
        };
        int result = _highestFilter.ApplyFilter(data);//bruger filter
        Assert.That(result, Is.EqualTo(expected));//tjekker resultatet
    }
}
