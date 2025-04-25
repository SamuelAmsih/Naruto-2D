using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private float interval;   // sekunder per frame
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    
    public void PlayAnimation(Sprite[] newSprites, float fps)
    {
        if (newSprites == null || newSprites.Length == 0) return;
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        CancelInvoke();              // stoppa tidigare
        sprites  = newSprites;
        interval = 1f / fps;         // fps → sek/frame
        frame    = 0;
        spriteRenderer.sprite = sprites[0];
        InvokeRepeating(nameof(Animate), interval, interval);
    }

    private void Animate()
    {
        frame = (frame + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[frame];
    }
}
