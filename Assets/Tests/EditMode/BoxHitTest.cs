
using NUnit.Framework;
using UnityEngine;
using System.Reflection;


namespace Tests
{

public class BoxHitTest
{
    private GameObject boxGO;
    private BoxHit boxHit;
    private GameObject prefab;
    private GameObject clone;
    private int initialMaxHits;
    private string prefabName;

    [SetUp]
    public void SetUp()
    {
        // Skapa GameObject med BoxHit-komponenten
        boxGO = new GameObject("Box");
        boxHit = boxGO.AddComponent<BoxHit>();
    }

    [TearDown]
    public void TearDown()
    {
        // Rensa upp instanser efter test
        if (clone != null) Object.DestroyImmediate(clone);
        if (prefab != null) Object.DestroyImmediate(prefab);
        Object.DestroyImmediate(boxGO);
    }

    [Test]
    public void Hit_WithoutItem_OnlyDecrementsMaxHits()
    {
        boxHit.item = null;
        initialMaxHits = boxHit.maxHits;

        // Anropa privat metod Hit via reflection
        InvokeHit();

        Assert.AreEqual(initialMaxHits - 1, boxHit.maxHits,
            "maxHits ska minska med 1 även om item är null");
    }

    [Test]
    public void Hit_WithItem_InstantiatesItemAtPosition()
    {
        // Skapa en "prefab" att instantiere
        prefabName = "TestItem";
        prefab = new GameObject(prefabName);
        boxHit.item = prefab;
        initialMaxHits = boxHit.maxHits;

        InvokeHit();

        // maxHits ska alltid minska
        Assert.AreEqual(initialMaxHits - 1, boxHit.maxHits,
            "maxHits ska minska med 1 när item finns");

        // Kontrollera att ett klonat objekt med rätt namn finns
        clone = GameObject.Find(prefabName + "(Clone)");
        Assert.IsNotNull(clone, "Ett klonat objekt ska ha instantiats");

        // Kontrollera att instansen placeras på boxens position
        Assert.AreEqual(boxGO.transform.position, clone.transform.position,
            "Det instanserade objektet ska ha samma position som boxen");
    }

    // Hjälpfunktion för att anropa Hit()
    private void InvokeHit()
    {
        MethodInfo hitMethod = typeof(BoxHit)
            .GetMethod("Hit", BindingFlags.NonPublic | BindingFlags.Instance);
        hitMethod.Invoke(boxHit, null);
    }
}


}
