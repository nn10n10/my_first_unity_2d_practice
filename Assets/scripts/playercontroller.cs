using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public float speed;
    public float jumpforce;
    public Transform groundcheck;
    public LayerMask ground;
    public bool isGround, isJump;
    bool jumpPressed;
    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        // anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void  Update() 
    {
        if(Input.GetButton("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundcheck.position,0.1f,ground);
        GroundMovement();
        Jump();
    }

    void Movement(){
        if(Input.GetButtonDown("Jump")&& jumpCount>0){
            jumpPressed = true;
            rb.velocity = new Vector2(rb.velocity.x,jumpforce * Time.deltaTime);
        }
    }
    void GroundMovement(){
        float horizontalmove= Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalmove*speed * Time.deltaTime,rb.velocity.y);
        if(horizontalmove != 0){
            transform.localScale = new Vector3(horizontalmove,1,1);
        }
    }
    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump  = true;
            rb.velocity = new Vector2(rb.velocity.x,jumpforce* Time.deltaTime);
            jumpCount--;
            jumpPressed = false;
        }
        else if(jumpPressed && jumpCount>0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpforce* Time.deltaTime);
            jumpCount--;
            jumpPressed = false;
        }

    }

}
