using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask JumpableGround;

    private float dirX = 0f;

    [SerializeField] private float jumpforce = 14f;
    [SerializeField] private float movespeed = 7f;
    [SerializeField] private float walkspeed = 1f;

    private enum MovementState { Idle, running, jump, falling }

    [SerializeField] private AudioSource JumpSoundEffect;

    public override bool Equals(object obj)
    {
        return obj is PlayerMovement movement &&
               base.Equals(obj) &&
               EqualityComparer<SpriteRenderer>.Default.Equals(sprite, movement.sprite);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent < SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
   private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

        UpdateAnimationstate();
    }

    private void UpdateAnimationstate()
    {
        MovementState State;

        if(dirX > 0f)
        {
            State = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            State = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.Idle;
        }
        if (rb.velocity.y > .1f)
        {
            State = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f) 
        {
            State = MovementState.falling;
        }


        anim.SetInteger("state", (int)State);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}
