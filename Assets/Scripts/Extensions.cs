using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic) {
            return false;
        }

        float Radius = 0.05f;
        float Distance = 0.1f;

        RaycastHit2D hit =Physics2D.CircleCast(rigidbody.position, Radius, direction, Distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }
}
