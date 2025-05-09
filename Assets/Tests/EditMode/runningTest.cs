using NUnit.Framework;
using UnityEngine;

public class PlayerMovements_RunningTest
{
    [Test]
    public void Running_ReturnsTrue_WhenVelocityXIsHigh()
    {
        var go = new GameObject();
        var pm = go.AddComponent<PlayerMovments>();
        var field = typeof(PlayerMovments).GetField("velocity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(pm, new Vector2(0.3f, 0));
        Assert.IsTrue(pm.Running);
        Object.DestroyImmediate(go);
    }
}
