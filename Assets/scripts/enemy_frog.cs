using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_frog : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed,jumpforce;
    private bool Facedleft = true;
    public Transform leftpointx,rightpointx;
    private float leftx, rightx;
    private Animator anim;
    private Collider2D coll;
    public LayerMask ground;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        leftx = leftpointx.position.x;
        rightx = rightpointx.position.x;
        Destroy(leftpointx.gameObject);
        Destroy(rightpointx.gameObject);
    }

    
    void Update()
    {
        movement();
        SwitchAnim();
    }
    void movement()
    {
        if (Facedleft)
        {
            if (coll.IsTouchingLayers(ground))
            {
               anim.SetBool("jumping", true);
               rb.velocity = new Vector2(-Speed, jumpforce);

            }
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Facedleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, jumpforce);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Facedleft = true;
            }
        }
    }
    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f )
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("falling", false);
            }
        }
    }
}
