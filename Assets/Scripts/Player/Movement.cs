using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    public float speed;
    private SpriteRenderer sprite;
    private Vector2 direction;
    private Vector2 lastDirection = Vector2.zero;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
       
        if(direction.magnitude > 0f) 
        {
            lastDirection = direction;
            if(direction.x > 0f)
            {
                sprite.flipX = false;
                anim.SetInteger("Base", 1);     
            } 
            else if (direction.x < 0f) 
            {
                sprite.flipX = true;
                anim.SetInteger("Base", 1);           
            } 
        }
        else 
        {
            if(lastDirection.x > 0f)
            {
                sprite.flipX = false;
                anim.SetInteger("Base", 0);     
            } 
            else if (lastDirection.x < 0f) 
            {
                sprite.flipX = true;
                anim.SetInteger("Base", 0);           
            } 
        }
    }

    void FixedUpdate() {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }
}
