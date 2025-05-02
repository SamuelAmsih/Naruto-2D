using System.Collections;
using Codice.Client.Common.GameUI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;


    private DeathAnimation deathAnimation;

    public bool Big => bigRenderer.enabled;
    public bool Small => smallRenderer.enabled;
    public bool Dead => deathAnimation.enabled;
    
    public void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();

        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
    }

    public void Hit()
    {
        Debug.Log("Player.Hit() called");
        if (Big) {
            Debug.Log("Player is Big, shrinking");
            Shrink();
        } else {
            Debug.Log("Player is Small, dying");
            Death();
        }
    }

    private void Shrink()
    {
        // todo
    }

private void Death()
    {
        Debug.Log("Player.Death() called");
        
        // First play death animation if available
        if (smallRenderer.enabled && smallRenderer.gameObject.activeSelf)
        {
            Debug.Log("Playing small renderer death animation");
            smallRenderer.PlayDeathAnimation();
        }
        else if (bigRenderer.enabled && bigRenderer.gameObject.activeSelf)
        {
            Debug.Log("Playing big renderer death animation");
            bigRenderer.PlayDeathAnimation();
        }
        
        // Start the death sequence coroutine
        StartCoroutine(DelayedDeathSequence());
    }
    
    private IEnumerator DelayedDeathSequence()
    {
        // Wait for death animation to play a bit
        yield return new WaitForSeconds(0.5f);
        
        Debug.Log("Delayed death sequence running");
        
        // Now disable renderers and enable death animation
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        
        Debug.Log("Death animation enabled: " + deathAnimation.enabled);
        
        // Reset the level after a delay
        GameManager.Instance.ResetLevel(3f);
        Debug.Log("Reset level called");
    }
}
