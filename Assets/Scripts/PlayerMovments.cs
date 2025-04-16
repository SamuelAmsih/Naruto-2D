using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Camera camera;
    

    private float inputAxis;
    private Vector2 velocity;

    public float movementSpeed = 8f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();
    }

    //handleing X-axis movements
    private void HorizontalMovement()
    {
            inputAxis = Input.GetAxis("Horizontal");
            velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);
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
}
