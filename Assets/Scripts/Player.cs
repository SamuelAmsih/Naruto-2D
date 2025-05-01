using Codice.Client.Common.GameUI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;

    public DeathAnimation deathAnimation;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    
    public void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();

    }

    public void Hit()
    {
        if (big) {
            Shrink();
        } else {
            Death();
        }
    }

    private void Shrink()
    {
        // todo
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = false;

        GameManager.Instance.ResetLevel(5f);
    }
}
