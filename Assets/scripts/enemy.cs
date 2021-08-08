using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected AudioSource deathAudio;
    protected Animator Anim;

    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }
    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
    public void JumpOn(){

        Anim.SetTrigger("death");
        deathAudio.Play();
    }

}
