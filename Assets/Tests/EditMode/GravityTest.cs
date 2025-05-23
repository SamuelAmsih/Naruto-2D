using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerGravityTests
    {
        private GameObject go;
        private PlayerMovments pm;

        [SetUp]
        public void SetUp()
        {
            go = new GameObject("Player");
            pm = go.AddComponent<PlayerMovments>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Gravity_CalculatesCorrectly()
        {
            // ARRANGE
            pm.MaxJumpHeight = 5f;
            pm.MaxJumpTime   = 1f;

             Debug.Log($"MaxJumpHeight = {pm.MaxJumpHeight}, MaxJumpTime = {pm.MaxJumpTime}");

            // Förväntat värde enligt formeln i ditt skript
            float expected = (-3.5f * pm.MaxJumpHeight)
                           / Mathf.Pow((pm.MaxJumpTime / 2f), 2);

            // ACT
            float actual = pm.Gravity;

            Debug.Log($"Expected: {expected}, Actual: {actual}");

            // ASSERT
            Assert.AreEqual(expected, actual, 0.0001f,
                "Gravity-egenskapen gav inte det förväntade värdet.");
        }
    }
}
