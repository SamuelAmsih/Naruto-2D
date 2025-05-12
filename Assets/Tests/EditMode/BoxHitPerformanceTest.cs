using NUnit.Framework;
using UnityEngine;
using Unity.PerformanceTesting;

namespace PerfTest
{



public class BoxHitPerformanceTests
{
    private GameObject _boxGO;
    private BoxHit _boxHit;

    [SetUp]
    public void SetUp()
    {
        _boxGO = new GameObject("Box");
        _boxHit = _boxGO.AddComponent<BoxHit>();
        _boxHit.item = null;         // Exkludera instansiering för ren mikromätning
        _boxHit.maxHits = 10000;     // Hög belastning
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_boxGO);
    }

    [Test, Performance]
    public void Hit_Method_ExecutesUnderThreshold()
    {
        Measure.Method(() => _boxHit.InvokeHitViaReflection())
            .WarmupCount(5)
            .MeasurementCount(20)
            .GC()
            .Run();
    }
}

// Hjälpmetod för att anropa privat Hit() via reflection
public static class BoxHitExtensions
{
    public static void InvokeHitViaReflection(this BoxHit box)
    {
        var mi = typeof(BoxHit).GetMethod(
            "Hit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        mi.Invoke(box, null);
    }
}
}
