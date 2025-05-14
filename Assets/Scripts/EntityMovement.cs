using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction = Vector2.left;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private SpriteRenderer spriteRenderer;
    
    // Add a cooldown for direction changes
    private float directionChangeCooldown = 0f;
    private float cooldownDuration = 0.5f; // Half a second cooldown
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false;
    }
    
    // Keep existing methods the same
    private void OnBecameVisible()
    {
        enabled = true;
    }
    
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    
    private void OnEnable()
    {
        rigidbody.WakeUp();
    }
     
    private void OnDisable()
    {
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.Sleep();
    }
    
    private void Update()
    {
        // Update cooldown timer
        if (directionChangeCooldown > 0)
        {
            directionChangeCooldown -= Time.deltaTime;
        }
        
        // Update sprite direction
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x > 0;
        }
    }
    
    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        
        if (rigidbody.Raycast(Vector2.down)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
    
    // Modified collision handler with cooldown
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Skip player collisions
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        // Only change direction if cooldown has expired
        if (directionChangeCooldown <= 0)
        {
            // Check if this is a side collision (better than changing on any collision)
            if (collision.contacts.Length > 0)
            {
                Vector2 normal = collision.contacts[0].normal;
                
                // Only change direction on side collisions
                if (Mathf.Abs(normal.x) > 0.5f)
                {
                    // Change direction
                    direction = -direction;
                    
                    // Add a small position offset to prevent sticking
                    Vector2 newPosition = rigidbody.position + new Vector2(direction.x * 0.1f, 0);
                    rigidbody.position = newPosition;
                    
                    // Reset cooldown
                    directionChangeCooldown = cooldownDuration;
                    
                    Debug.Log("Direction changed, cooldown started");
                }
            }
        }
    }
}