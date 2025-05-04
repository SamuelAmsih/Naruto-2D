
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private AnimatedSprite anim;
    private PlayerMovments mov;
    private SpriteRenderer spriteRend;

    public Sprite[] IdleFrames;
    public float   IdleFps = 2f;
    public Sprite[] RunFrames;
    public float   RunFps = 10f;
    public Sprite[] JumpFrames;
    public float   JumpFps = 4f;
    public Sprite[] DeadFrames;
    public float   DeadFps = 5f;

    private enum State { Idle, Run, Jump, Dead }
    private State currentState;

    private void Awake()
    {
        anim       = GetComponent<AnimatedSprite>();
        mov        = GetComponentInParent<PlayerMovments>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentState = State.Idle;
        anim.PlayAnimation(IdleFrames, IdleFps);
    }

    private void LateUpdate()
    {
        if (currentState == State.Dead) return;

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

    // Metoder för att dölja/visa spriten
    public void Show()  => spriteRend.enabled = true;
    public void Hide()  => spriteRend.enabled = false;
    public void Toggle() => spriteRend.enabled = !spriteRend.enabled;
    public bool Visible => spriteRend.enabled;
}
