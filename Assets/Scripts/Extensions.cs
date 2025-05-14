using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default", "player", "Ground", "Background");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.bodyType == RigidbodyType2D.Kinematic)
            return false;

        float radius = 0.05f;
        float distance = 0.08f; // lite längre för bättre träff

        // Hämta kolliderns bounds för att justera ursprung
        Collider2D collider = rigidbody.GetComponent<Collider2D>();
        if (collider == null)
            return false;

        Bounds bounds = collider.bounds;
        Vector2 origin = rigidbody.position;

        // Justera ursprung beroende på riktning
        if (direction == Vector2.down)
            origin = new Vector2(bounds.center.x, bounds.min.y);
        else if (direction == Vector2.up)
            origin = new Vector2(bounds.center.x, bounds.max.y);
        else if (direction == Vector2.left)
            origin = new Vector2(bounds.min.x, bounds.center.y);
        else if (direction == Vector2.right)
            origin = new Vector2(bounds.max.x, bounds.center.y);

        // Gör casten
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);

#if UNITY_EDITOR
        Debug.DrawRay(origin, direction * distance, hit.collider != null ? Color.green : Color.red, 0.1f);
#endif

        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    // dot product collision test (ex: "am I hitting something above me?")
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
