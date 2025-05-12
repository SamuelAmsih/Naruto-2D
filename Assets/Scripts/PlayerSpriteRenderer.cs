// PlayerSpriteRenderer.cs
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteRenderer : MonoBehaviour
{
    private AnimatedSprite anim;
    private PlayerMovments mov;
    private SpriteRenderer spriteRend;
    private Player player;

    public Sprite[] IdleFrames;
    public float   IdleFps = 2f;
    public Sprite[] RunFrames;
    public float   RunFps = 10f;
    public Sprite[] JumpFrames;
    public float   JumpFps = 4f;
    public Sprite[] DeadFrames;
    public float   DeadFps = 5f;
    public Sprite[] RasenganFrames;
    public float RasenganFps = 20f;

    private enum State { Idle, Run, Jump, Dead , Rasengan }
    private State currentState;

    private void Awake()
    {
       
        anim       = GetComponent<AnimatedSprite>();
        mov        = GetComponentInParent<PlayerMovments>();
        spriteRend = GetComponent<SpriteRenderer>();
        player     = GetComponentInParent<Player>();

        if (spriteRend == null)
            Debug.LogError($"[{name}] Kunde inte hitta SpriteRenderer-komponenten!");
    }

    private void Start()
    {
       
        currentState = State.Idle;
        anim.PlayAnimation(IdleFrames, IdleFps);
    }

    private void LateUpdate()
    {
        if (currentState == State.Dead || currentState == State.Rasengan) return;

      
        State newState = mov.Jumping
            ? State.Jump
            : mov.Running
                ? State.Run
                : State.Idle;

        if (newState != currentState)
        {
            currentState = newState;
            switch (currentState)
            {
                case State.Idle:
                    anim.PlayAnimation(IdleFrames, IdleFps);
                    break;
                case State.Run:
                    anim.PlayAnimation(RunFrames, RunFps);
                    break;
                case State.Jump:
                    anim.PlayAnimation(JumpFrames, JumpFps);
                    break;
                case State.Dead:
                    anim.PlayAnimation(DeadFrames, DeadFps);
                    break;
            }
        }
    }

    public void PlayDeathAnimation()
    {
        currentState = State.Dead;
        anim.PlayAnimation(DeadFrames, DeadFps);
    }

    public void PlayRasengan()
    {
        if (currentState == State.Dead || currentState == State.Rasengan || player.IsTransforming || player.Big) return;

        currentState = State.Rasengan;
        anim.PlayAnimation(RasenganFrames, RasenganFps);

        StartCoroutine(PlayRasenganSounds());
    }

    public IEnumerator PlayRasenganRoutine(float duration)
    {
        PlayRasengan();

        yield return new WaitForSeconds(duration);

        currentState = State.Idle;
        anim.PlayAnimation(IdleFrames, IdleFps);
    }

    private IEnumerator PlayRasenganSounds()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.kagenojutsu);

        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.rasengan);

        yield return new WaitForSeconds(2.35f);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.rasengan2);
    }

    public bool IsRasenganActive()
    {
        return currentState == State.Rasengan;
    }

  
    public void Show()
    {
     
        if (spriteRend == null)
            spriteRend = GetComponent<SpriteRenderer>();

        if (spriteRend != null)
            spriteRend.enabled = true;
    }

    public void Hide()  => spriteRend.enabled = false;
    public void Toggle() => spriteRend.enabled = !spriteRend.enabled;
    public bool Visible => spriteRend.enabled;
}