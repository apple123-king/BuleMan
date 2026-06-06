using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprit;
    private BoxCollider2D coll;

    [SerializeField]private LayerMask jumpableGround;
    private enum MovementState { idle, run, jump, drop }

    public float Speed = 5f;
    public float JumpForce = 7f;
    private float moveInput;
    [SerializeField] private AudioSource jumpSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprit = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        //jumpSoundEffect = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)&&IsGrounded())
        {   
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        AnimUpdate();
    }

    void AnimUpdate()
    {
        MovementState state;
        if (moveInput > 0f)
        {
            sprit.flipX = false;
            state = MovementState.run;
        }
        else if (moveInput < 0f)
        {
            sprit.flipX = true;
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
        //print((int)state+state);
        anim.SetInteger("statue", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }



}