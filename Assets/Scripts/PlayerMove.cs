using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState { idle, run, jump, drop }

    public float Speed = 5f;
    public float JumpForce = 7f;

    [Header("Jump Assist")]
    [SerializeField] private bool allowDoubleJump = true;
    [SerializeField] private int extraJumpCount = 1;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;
    [SerializeField] private float fallGravityMultiplier = 1.4f;
    [SerializeField] private float lowJumpGravityMultiplier = 1.15f;

    private float moveInput;
    [SerializeField] private AudioSource jumpSoundEffect;

    private float speedMultiplier = 1f;
    private float defaultGravityScale = 1f;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private int extraJumpsRemaining;
    private Coroutine speedBoostCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        if (rb != null)
        {
            defaultGravityScale = rb.gravityScale;
        }

        extraJumpsRemaining = extraJumpCount;
    }

    void Update()
    {
        if (rb == null)
        {
            return;
        }

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed * speedMultiplier, rb.velocity.y);

        bool grounded = IsGrounded();
        if (grounded)
        {
            coyoteCounter = coyoteTime;
            extraJumpsRemaining = extraJumpCount;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && CanJump())
        {
            Jump(grounded);
        }

        UpdateGravity();
        AnimUpdate();
    }

    private bool CanJump()
    {
        return coyoteCounter > 0f || (allowDoubleJump && extraJumpsRemaining > 0);
    }

    private void Jump(bool grounded)
    {
        if (!grounded && coyoteCounter <= 0f)
        {
            extraJumpsRemaining--;
        }

        jumpBufferCounter = 0f;
        coyoteCounter = 0f;
        jumpSoundEffect?.Play();
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }

    private void UpdateGravity()
    {
        if (rb.velocity.y < -0.1f)
        {
            rb.gravityScale = defaultGravityScale * fallGravityMultiplier;
        }
        else if (rb.velocity.y > 0.1f && !Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = defaultGravityScale * lowJumpGravityMultiplier;
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
        }
    }

    private void AnimUpdate()
    {
        MovementState state;
        if (moveInput > 0f)
        {
            if (sprite != null)
            {
                sprite.flipX = false;
            }
            state = MovementState.run;
        }
        else if (moveInput < 0f)
        {
            if (sprite != null)
            {
                sprite.flipX = true;
            }
            state = MovementState.run;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.drop;

        }

        if (anim != null)
        {
            anim.SetInteger("statue", (int)state);
        }
    }

    private bool IsGrounded()
    {
        return coll != null && Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private System.Collections.IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        speedMultiplier = Mathf.Max(0.1f, multiplier);
        yield return new WaitForSeconds(Mathf.Max(0f, duration));
        speedMultiplier = 1f;
        speedBoostCoroutine = null;
    }

}
