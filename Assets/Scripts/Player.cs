using UnityEngine;
using System.Collections;
using Codice.Client.Common.GameUI;

public class Player : MonoBehaviour
{
    [Header("Referenser till sprite-renders")]
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

   
    public bool Big   => bigRenderer != null && bigRenderer.Visible;
    public bool Small => smallRenderer != null && smallRenderer.Visible;
    public bool Dead  => deathAnimation != null && deathAnimation.enabled;

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

        if (Small)
            smallRenderer.PlayDeathAnimation();
        else if (Big)
            bigRenderer.PlayDeathAnimation();

        StartCoroutine(DelayedDeathSequence());
    }

    private IEnumerator DelayedDeathSequence()
    {
        yield return new WaitForSeconds(0.5f);

        // Dölj båda och aktivera DeathAnimation
        smallRenderer.Hide();
        bigRenderer.Hide();
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void Grow()
{
    Debug.Log("Player.Grow() called");
    smallRenderer.Hide();
    bigRenderer.Show();
    activeRenderer = bigRenderer;
     
    // Correct way to set a horizontal capsule collider
    capsuleCollider.direction = CapsuleDirection2D.Horizontal;
    capsuleCollider.size = new Vector2(0.5f, 0.4f); // Use positive values
    capsuleCollider.offset = new Vector2(0f, 0.4f); // Adjust as needed
    
    StartCoroutine(ScaleAnimation());
}

    private void Shrink()
    {
        Debug.Log("Player.Shrink() called");

        smallRenderer.Show();
        bigRenderer.Hide();
        activeRenderer = smallRenderer;

        // Uppdatera collider för liten form
        capsuleCollider.direction = CapsuleDirection2D.Vertical;
        capsuleCollider.size   = new Vector2(0.6f, 0.9f);
        capsuleCollider.offset = new Vector2(-0.45f, 0.57f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
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
    }
}
