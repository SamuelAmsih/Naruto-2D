using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerJumpForceTests
    {
        private GameObject go;
        private PlayerMovments pm;

        [SetUp]
        public void SetUp()
        {
            // Skapa GameObject med komponenten vi vill testa
            go = new GameObject("Player");
            pm = go.AddComponent<PlayerMovments>();
        }

        [TearDown]
        public void TearDown()
        {
            // Rensa upp efter testet
            Object.DestroyImmediate(go);
        }

        [Test]
        public void JumpForce_CalculatesCorrectly()
        {
            // ARRANGE: ställ in de parametrar formeln använder
            pm.MaxJumpHeight = 5f;
            pm.MaxJumpTime   = 1f;

            // Förväntat värde enligt formeln i ditt skript
            float expected = (2f * pm.MaxJumpHeight) / (pm.MaxJumpTime / 2f);

            // ACT: hämta det beräknade värdet
            float actual = pm.JumpForce;

            // ASSERT: jämför med liten tolerans
            Assert.AreEqual(expected, actual, 0.0001f,
                "JumpForce-egenskapen gav inte det förväntade värdet.");
        }
    }
}

