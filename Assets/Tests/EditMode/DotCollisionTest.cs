using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ExtensionsDotTest
    {
        [Test]
        public void DotTest_ReturnsTrue_WhenOtherInDirection()
        {
            // Arrange
            var go1 = new GameObject();
            var go2 = new GameObject();
            go1.transform.position = Vector3.zero;
            go2.transform.position = new Vector3(1f, 0f, 0f);

            // Act
            bool result = go1.transform.DotTest(go2.transform, Vector2.right);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DotTest_ReturnsFalse_WhenOtherOppositeDirection()
        {
            var go1 = new GameObject();
            var go2 = new GameObject();
            go1.transform.position = Vector3.zero;
            go2.transform.position = new Vector3(-1f, 0f, 0f);

            bool result = go1.transform.DotTest(go2.transform, Vector2.right);
            Assert.IsFalse(result);
        }
    }
}


