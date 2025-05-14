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

        // Add these variables to your Player class
    private bool isFacingRight = true;
    private Vector2 rightFacingBigOffset = new Vector2(-0.5f, 0.4f);  // Your current big form offset
    private Vector2 leftFacingBigOffset = new Vector2(-1.5f, 0.4f);    // Mirrored X offset for big form

// Add this method to update the facing direction
    public void UpdateFacingDirection(bool facingRight)
    {
        // Only update if direction actually changed
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            
            // Only update collider offset if in big form
            if (Big)
            {
                UpdateBigColliderOffset();
            }
        }
    }

    // Simplified method that only updates the collider for big form
    private void UpdateBigColliderOffset()
    {
        if (capsuleCollider == null) return;
        
        // Only adjust offset for big form - small form remains unchanged
        if (Big)
        {
            capsuleCollider.offset = isFacingRight ? rightFacingBigOffset : leftFacingBigOffset;
        }
    }

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

// Add these variables to your Player class
[Header("Invincibility Settings")]
public float invincibilityDuration = 2f;
public float blinkRate = 0.1f;
private bool isInvincible = false;

// Modify your Hit method
public void Hit()
{
    Debug.Log("Player.Hit() called");
    
    // If already invincible, ignore the hit
    if (isInvincible) return;
    
    if (Big)
    {
        Debug.Log("Player is Big, shrinking");
        Shrink();
        
        // Start invincibility after shrinking
        StartCoroutine(InvincibilityRoutine());
    }
    else
    {
        Debug.Log("Player is Small, dying");
        Death();
    }
}

// Add this coroutine to handle invincibility
// Modify this coroutine to handle invincibility
    private IEnumerator InvincibilityRoutine()
{
    isInvincible = true;
    
    // Get reference to sprite renderer
    SpriteRenderer spriteRenderer = smallRenderer.GetComponent<SpriteRenderer>();
    if (spriteRenderer == null)
    {
        // If the SpriteRenderer isn't directly on smallRenderer, try to find it in children
        spriteRenderer = smallRenderer.GetComponentInChildren<SpriteRenderer>();
    }
    
    // Store original color for later restoration
    Color originalColor = Color.white;
    if (spriteRenderer != null)
    {
        originalColor = spriteRenderer.color;
    }
    
    // Blink the sprite during invincibility period
    float invincibilityEndTime = Time.time + invincibilityDuration;
    
    if (spriteRenderer != null)
    {
        // Blink by changing alpha
        while (Time.time < invincibilityEndTime)
        {
            // Toggle alpha between half and full
            Color tempColor = spriteRenderer.color;
            tempColor.a = tempColor.a > 0.5f ? 0.3f : 1f;
            spriteRenderer.color = tempColor;
            
            yield return new WaitForSeconds(blinkRate);
        }
        
        // Explicitly restore original color
        spriteRenderer.color = originalColor;
    }
    else
    {
        // Fallback if sprite renderer not found - just wait
        yield return new WaitForSeconds(invincibilityDuration);
    }
    
    isInvincible = false;
    Debug.Log("Invincibility ended");
    
    // Double-check that the sprite is fully visible
    if (spriteRenderer != null)
    {
        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f; // Force full opacity
        spriteRenderer.color = finalColor;
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
        
        // Set the collider properties for big form
        capsuleCollider.direction = CapsuleDirection2D.Horizontal;
        capsuleCollider.size = new Vector2(0.9f, 0.5f);
        
        // Set the initial offset based on current facing direction
        capsuleCollider.offset = isFacingRight ? rightFacingBigOffset : leftFacingBigOffset;
        
        StartCoroutine(ScaleAnimation());
    }

 // Modify your Shrink method to start invincibility after the scale animation
    private void Shrink()
    {
        Debug.Log("Player.Shrink() called");

        StartCoroutine(PlayShrinkSounds());

        smallRenderer.Show();
        bigRenderer.Hide();
        activeRenderer = smallRenderer;

        // Update collider for small form
        capsuleCollider.direction = CapsuleDirection2D.Vertical;
        capsuleCollider.size = new Vector2(0.6f, 0.9f);
        capsuleCollider.offset = new Vector2(-0.45f, 0.57f);

        // Start scale animation and chain to invincibility afterwards
        StartCoroutine(ScaleAnimationThenInvincibility());
    }

    // New method to chain animations
    private IEnumerator ScaleAnimationThenInvincibility()
    {
        yield return StartCoroutine(ScaleAnimation());
        
        // Start invincibility after scale animation completes
        StartCoroutine(InvincibilityRoutine());
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
