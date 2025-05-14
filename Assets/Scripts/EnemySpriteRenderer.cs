using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpriteRenderer : MonoBehaviour
{
    private Sprite[] currentFrames;

    private AnimatedSprite anim;
    private EntityMovement move;

    public Sprite[] walkFrames;
    public float walkFps = 6f;

    public Sprite[] deathFrames;
    public float deathFps = 5f;

    private enum State { Walk, Dead }
    private State currentState;

    private void Awake()
    {
        anim = GetComponent<AnimatedSprite>();
        move = GetComponent<EntityMovement>();
    }

    private void Start()
    {
        currentState = State.Walk;
        anim.PlayAnimation(walkFrames, walkFps);
        Debug.Log("EnemySpriteRenderer: Start() called");

    }

   private void LateUpdate()
    {
        if (currentState == State.Dead) return;

        if (currentState == State.Walk && currentFrames != walkFrames)
        {
        currentFrames = walkFrames;
        anim.PlayAnimation(walkFrames, walkFps);
        }
    }


    public void PlayDeathAnimation()
    {
    currentState = State.Dead;
    currentFrames = deathFrames;
    anim.PlayAnimation(deathFrames, deathFps);
    }

}
