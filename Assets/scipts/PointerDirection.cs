using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerDirection : MonoBehaviour
{

    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private float dirX = 0f;

    [SerializeField] private float movespeed = 7f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y);

        if (dirX > 0f)
        {
            sprite.flipX = true;
        }
        else if (dirX < 0f)
        {
            sprite.flipX = false;
        }
    }
}
