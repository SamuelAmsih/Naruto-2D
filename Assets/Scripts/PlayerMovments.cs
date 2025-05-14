//using log4net.Util;
using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private Player player;
    

    private float inputAxis;
    private Vector2 velocity;

    public float MovementSpeed = 6f;
    public float MaxJumpHeight = 5f;
    public float MaxJumpTime = 1f;
    public float JumpForce => (2f * MaxJumpHeight) / (MaxJumpTime / 2f);
    public float Gravity => (-3.5f * MaxJumpHeight) / Mathf.Pow((MaxJumpTime / 2f), 2);
    
    public bool Grounded { get; private set; }
    public bool Jumping { get; private set; }
    public bool Running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    Vector2 groundCheckOffset = new Vector2(-0.2f, 0f);
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        HorizontalMovement();

        Grounded = rigidbody.Raycast(Vector2.down, groundCheckOffset);

        if (Grounded){
            GroundedMovement();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player.IsTransforming) return;
            if (player.activeRenderer.IsRasenganActive()) return;
            player.PlayRasengan();
            velocity = Vector2.zero;
            return;
        }

        ApplyGravity();
    }

    public UnityEngine.Transform kuyobi;
    public UnityEngine.Transform naruto;
    //handleing X-axis movements
    //handleing X-axis movements
    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = inputAxis * MovementSpeed;//Mathf.MoveTowards(velocity.x, inputAxis * MovementSpeed, MovementSpeed * Time.deltaTime)/(3/2);

        //animation rotation
        if (velocity.x > 0f)
        {
            naruto.eulerAngles = Vector3.zero;
            kuyobi.eulerAngles = Vector3.zero;
            
            // Update the player direction
            player.UpdateFacingDirection(true);
            
        } 
        else if (velocity.x < 0f) 
        {
            naruto.eulerAngles = new Vector3(0f, 180f, 0f);
            kuyobi.eulerAngles = new Vector3(0f, 180f, 0f);
            
            // Update the player direction
            player.UpdateFacingDirection(false);
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        Jumping = velocity.y > 0f;


        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = JumpForce;
            Jumping = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
        }
    }

    private void ApplyGravity()
    {
        
        velocity.y += Gravity *  Time.deltaTime;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = JumpForce * (3/2);
                Jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }

    #if UNITY_EDITOR
public void TestFlip(float simulatedInput)
{
    // bypass Input.GetAxis
    velocity.x = Mathf.MoveTowards(0, simulatedInput * MovementSpeed, MovementSpeed * Time.deltaTime) / 1.5f;

    if (velocity.x > 0f)
    {
        naruto.eulerAngles = Vector3.zero;
        kuyobi.eulerAngles = Vector3.zero;
    }
    else if (velocity.x < 0f)
    {
        naruto.eulerAngles = new Vector3(0f, 180f, 0f);
        kuyobi.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}
#endif

}

//Filip
