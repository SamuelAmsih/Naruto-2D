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

        if (Small)
            smallRenderer.PlayDeathAnimation();
        else if (Big)
            bigRenderer.PlayDeathAnimation();

        StartCoroutine(DelayedDeathSequence());

        AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
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
