using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private float inputAxis;
    private Vector2 velocity;

    public float movementSpeed = 8f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

        rigidbody.MovePosition(position);
    }
}
