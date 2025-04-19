using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.bodyType == RigidbodyType2D.Kinematic)
 {
            return false;
        }

        float Radius = 0.05f;
        float Distance = 0.01f;

        RaycastHit2D hit =Physics2D.CircleCast(rigidbody.position, Radius, direction.normalized, Distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    //dot-product-unit-testing for collision with objects above the character
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
