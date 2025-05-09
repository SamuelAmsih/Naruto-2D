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

     
        capsuleCollider.size   = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 1f);

        StartCoroutine(ScaleAnimation());
    }

    private void Shrink()
    {
        Debug.Log("Player.Shrink() called");

        smallRenderer.Show();
        bigRenderer.Hide();
        activeRenderer = smallRenderer;

        // Uppdatera collider för liten form
        capsuleCollider.size   = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(-0.5f, 0.6f);

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

    public void PlayRasengan()
    {
        if (activeRenderer != null)
        {
        activeRenderer.StartCoroutine(activeRenderer.PlayRasenganRoutine(4f));
        }
    }
}
