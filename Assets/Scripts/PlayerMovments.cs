using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Camera camera;
    

    private float inputAxis;
    private Vector2 velocity;

    public float MovementSpeed = 8f;
    public float MaxJumpHeight = 5f;
    public float MaxJumpTime = 1f;
    public float JumpForce => (2f * MaxJumpHeight) / (MaxJumpTime / 2f);
    public float Gravity => (-2f * MaxJumpHeight) / Mathf.Pow((MaxJumpTime / 2f), 2);
    
    public bool Grounded { get; private set; }
    public bool Jumping { get; private set; }
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();

        Grounded = rigidbody.Raycast(Vector2.down);

        if (Grounded){
            GroundedMovement();

        }

        ApplyGravity();

        
    }

    //handleing X-axis movements
    private void HorizontalMovement()
    {
            inputAxis = Input.GetAxis("Horizontal");
            velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * MovementSpeed, MovementSpeed * Time.deltaTime);
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        Jumping = velocity.y > 0f;


        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = JumpForce;
            Jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool Falling = velocity.y < 0f || !Input.GetButtonDown("Jump");
        float Multiplier = Falling ? 2f : 1f;
        velocity.y += Gravity * Multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 position  = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.8f, rightEdge.x -0.8f);

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
