using UnityEngine;
using System.Collections;
//using Codice.Client.Common.GameUI;

public class Player : MonoBehaviour
{
    [Header("Referenser till sprite-renders")]
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    public PlayerSpriteRenderer activeRenderer {get; private set;}

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

   
    public bool Big   => bigRenderer != null && bigRenderer.Visible;
    public bool Small => smallRenderer != null && smallRenderer.Visible;
    public bool Dead  => deathAnimation != null && deathAnimation.enabled;
    public bool IsTransforming {get; private set;}

    private void Awake()
    {
       
        deathAnimation  = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        
        smallRenderer.Show();
        bigRenderer.Hide();
        activeRenderer = smallRenderer;
    }

    public void Hit()
    {
        Debug.Log("Player.Hit() called");
        if (Big)
        {
            Debug.Log("Player is Big, shrinking");
            Shrink();
        }
        else
        {
            Debug.Log("Player is Small, dying");
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Player.Death() called");

        // Disable player movement
        GetComponent<PlayerMovments>().enabled = false;

        // Play death animation for current form
        if (Small)
            smallRenderer.PlayDeathAnimation();
        else if (Big)
            bigRenderer.PlayDeathAnimation();

        // Pause game elements
        PauseGameElements();

        // Play death sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
        
        // Pause background music
        AudioManager.Instance.StopMusic();

        // Start the death sequence
        StartCoroutine(DelayedDeathSequence());
    }

// Add this new method to pause all game elements
    private void PauseGameElements()
    {
        // Freeze all enemies by disabling their movement scripts
        EntityMovement[] enemies = Object.FindObjectsByType<EntityMovement>(FindObjectsSortMode.None);
        foreach (EntityMovement enemy in enemies)
        {
            enemy.enabled = false;
        }

        // Freeze any moving platforms or other objects with scripts
        MonoBehaviour[] movingObjects = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (MonoBehaviour script in movingObjects)
        {
            // Skip player-related scripts and this script
            if (script.gameObject != gameObject &&
                !(script is Player) &&
                !(script is PlayerSpriteRenderer) &&
                !(script is DeathAnimation))
            {
                // Disable scripts that might control movement
                // You may need to add specific script types to check for
                if (script is EntityMovement || script.GetType().Name.Contains("Movement"))
                {
                    script.enabled = false;
                }
            }
        }

        // Freeze physics objects (optional - use if you want to freeze falling objects too)
        Rigidbody2D[] rigidbodies = Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (Rigidbody2D rb in rigidbodies)
        {
            // Skip the player's rigidbody
            if (rb.gameObject != gameObject)
            {
                // Store original velocity if you need to restore it later
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.simulated = false; // This effectively freezes the physics object
            }
        }
    }

    private IEnumerator DelayedDeathSequence()
    {
        // Wait a moment before starting death animation
        yield return new WaitForSeconds(0.5f);

        // Hide renderers and activate death animation
        smallRenderer.Hide();
        bigRenderer.Hide();
        deathAnimation.enabled = true;
        
        // Let the death animation play for a bit
        //yield return new WaitForSeconds(1.5f);
        
        // Show game over UI if you have one
        //GameManager.Instance.ShowGameOverScreen();
        
        // Reset the level after a delay
        GameManager.Instance.ResetLevel(2f);
    }

    public void Grow()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.powerup);

        Debug.Log("Player.Grow() called");
        smallRenderer.Hide();
        bigRenderer.Show();
        activeRenderer = bigRenderer;
        
        // Correct way to set a horizontal capsule collider
        capsuleCollider.direction = CapsuleDirection2D.Horizontal;
        capsuleCollider.size = new Vector2(0.9f, 0.5f); // Use positive values
        capsuleCollider.offset = new Vector2(-0.5f, 0.4f); // Adjust as needed
        
        StartCoroutine(ScaleAnimation());
    }

    private void Shrink()
    {
        Debug.Log("Player.Shrink() called");

        StartCoroutine(PlayShrinkSounds());

        smallRenderer.Show();
        bigRenderer.Hide();
        activeRenderer = smallRenderer;

        // Uppdatera collider för liten form
        capsuleCollider.direction = CapsuleDirection2D.Vertical;
        capsuleCollider.size   = new Vector2(0.6f, 0.9f);
        capsuleCollider.offset = new Vector2(-0.45f, 0.57f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator PlayShrinkSounds()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.powerdown_1);

        yield return new WaitForSeconds(0.2f);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.powerdown_1_2);
    }

    private IEnumerator ScaleAnimation()
    {
        IsTransforming = true;
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                // Blinkande effekt genom att växla synlighet
                smallRenderer.Toggle();
                bigRenderer.Toggle();
            }

            yield return null;
        }

        // I slutet ställer vi in rätt state
        smallRenderer.Hide();
        bigRenderer.Hide();
        activeRenderer.Show();
        IsTransforming = false;
    }

    public void PlayRasengan()
    {
        if (activeRenderer != null)
        {
            activeRenderer.StartCoroutine(activeRenderer.PlayRasenganRoutine(4f));
        }
    }
}
