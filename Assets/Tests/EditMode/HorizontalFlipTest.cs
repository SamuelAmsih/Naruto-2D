using NUnit.Framework;
using UnityEngine;

public class PlayerMovements_HorizontalFlipTest
{
   [Test]
public void Character_Flips_Left_WhenMovingLeft()
{
    var go = new GameObject();
    var pm = go.AddComponent<PlayerMovments>();
    pm.naruto = new GameObject().transform;
    pm.kuyobi = new GameObject().transform;

    pm.TestFlip(-1f); // simulate left input

    Assert.AreEqual(180f, pm.naruto.eulerAngles.y, "Naruto should flip left.");
    Assert.AreEqual(180f, pm.kuyobi.eulerAngles.y, "Kuyobi should flip left.");

    Object.DestroyImmediate(go);
}

}
