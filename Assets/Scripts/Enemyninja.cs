using UnityEngine;

public class Enemyninja : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Enemy collided with: {collision.gameObject.name}");
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player tag detected");
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player == null)
            {
                Debug.Log("Player component not found on player object");
                return;
            }
            
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Debug.Log("Player stomped enemy from above");
                Flatten();
            } else {
                Debug.Log("Enemy hit player");
                player.Hit();
            }
        }
    }

    private void Flatten()
    {
        GetComponent<EnemySpriteRenderer>()?.PlayDeathAnimation();

        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
