using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private AnimatedSprite anim;       // vår “motor”
    private PlayerMovments mov;        // för input/state

    // konfigurerbara sprites & hastigheter 
    public Sprite[] IdleFrames;
    public float   IdleFps = 2f;

    public Sprite[] RunFrames;
    public float   RunFps = 10f;

    public Sprite[] JumpFrames;
    public float   JumpFps = 4f;
    public Sprite[] DeadFrames;        
    public float   DeadFps = 5f;

    // intern state-tracker 
    private enum State { Idle, Run, Jump, Dead}
    private State currentState;

    private void Awake()
    {
        anim = GetComponent<AnimatedSprite>();
        mov  = GetComponentInParent<PlayerMovments>();
        
    }

    private void Start()
    {
        currentState = State.Idle;
        // starta med idle
        anim.PlayAnimation(IdleFrames, IdleFps);
    }

    private void LateUpdate()
    {
        // om spelaren är död 
        if (currentState == State.Dead) 
        {
            return;
        }

        //  avgör ny state
        State newState = mov.Jumping
            ? State.Jump
            : mov.Running
                ? State.Run
                : State.Idle;

        // om förändring play ny animation
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
}
