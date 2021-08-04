using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class behavior : MonoBehaviour
{
    private Rigidbody2D rb;//生成变量“RB" 数据属性为RIGIDBODY2D----drag进去
    public float speed;//生成“为10.00的speed"的浮点型变量（speed = 10f）---unity里改数值
    public float jumpforce;//设置变量，不给初始值所以游戏中自己设置，“Jump"在EDIT-projectsettings-input-axes中找到大小写拼写
    private Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public int Cherry;
    public Text CherryNum;
    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurt)
        {
            Movement();//函数MOVEMENT每一帧都执行
        }
    }
    void FixedUpdate()
    {
       SwitchAnim();
    }

    void Movement()//函数 MOVEMENT
    {
        float horizontalmove = Input.GetAxis("Horizontal");//自定义浮点型参数取名//输入值 获得按键 横向的 -1.00~1.00
        float facedirection = Input.GetAxisRaw("Horizontal"); //自定义浮点型参数取名//GetAxisRaw直接获得-1 0 1


        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed* Time.deltaTime, rb.velocity.y);//变量“RB" 数据属性为RIGIDBODY2D中有速度值，水平值等于获得输入按键的值乘发布的速度变量，纵值不变//平滑不跳帧
            anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        //freeze z不转动  
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);//transform中的scacle的X值是facedirection

        }

        if(Input.GetButtonDown("Jump")&&coll.IsTouchingLayers(ground))//当用户按下“Jump”按键
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);//纵值等于如果按下跳跃键后，乘以跳跃参数或重力 平滑不跳帧
            anim.SetBool("jumping",true);
        }


    } 
    void SwitchAnim()
    {
        anim.SetBool("idle",false);

        if(rb.velocity.y<0.1f && !coll.IsTouchingLayers(ground)){
            anim.SetBool("idle",false);
            anim.SetBool("falling",true);
        }
        
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if(isHurt){
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0);
            if(Mathf.Abs(rb.velocity.x)<0.1f){
                anim.SetBool("hurt",false);
                anim.SetBool("idle",true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
    }
    //收集
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "collection")
        {
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text= Cherry.ToString();
        }
    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "enemies"){
        if(anim.GetBool("falling"))
        {
            Destroy(other.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, 100 * Time.deltaTime);
            anim.SetBool("jumping",true);
        }
        else if(transform.position.x < other.gameObject.transform.position.x)
        {
            rb.velocity = new Vector2(-5,rb.velocity.y);
            isHurt = true;
        }
        else if(transform.position.x > other.gameObject.transform.position.x)
        {
            rb.velocity = new Vector2(5,rb.velocity.y);
            isHurt = true;
        }

    }
}

}
