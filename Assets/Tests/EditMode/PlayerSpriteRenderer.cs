using NUnit.Framework;
using UnityEngine;


namespace Tests
{

public class PlayerSpriteRendererVisibilityTests
{
    private GameObject go;
    private PlayerSpriteRenderer psr;
    private SpriteRenderer rend;

    [SetUp]
    public void SetUp()
    {
        // Skapa GameObject med SpriteRenderer och PlayerSpriteRenderer
        go = new GameObject("PSRGO");
        rend = go.AddComponent<SpriteRenderer>();
        psr = go.AddComponent<PlayerSpriteRenderer>();
    }

    [TearDown]
    public void TearDown() => Object.DestroyImmediate(go);

    [Test]
    public void Show_SetsEnabledTrue()
    {
        // Bör vara avstängd från början
        rend.enabled = false;
        psr.Show();
        Assert.IsTrue(rend.enabled, "Show() ska sätta spriteRend.enabled = true");
        Assert.IsTrue(psr.Visible, "Visible ska vara true efter Show()");
    }

    [Test]
    public void Hide_SetsEnabledFalse()
    {
        // Säkerställ initialisering av spriteRend via Show()
        psr.Show();
        rend.enabled = true;

        psr.Hide();
        Assert.IsFalse(rend.enabled, "Hide() ska sätta spriteRend.enabled = false");
        Assert.IsFalse(psr.Visible, "Visible ska vara false efter Hide()");
    }

    [Test]
    public void Toggle_InvertsEnabled()
    {
        // Säkerställ initialisering av spriteRend
        psr.Show();
        // Testa vändning fram och tillbaka
        rend.enabled = true;
        psr.Toggle();
        Assert.IsFalse(rend.enabled, "Toggle() ska växla från true till false");
        psr.Toggle();
        Assert.IsTrue(rend.enabled, "Toggle() ska växla från false till true");
    }
}

}
