using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacontrol : MonoBehaviour
{
    Animator anim;
    public AudioClip bounce;
    public AudioSource musicSource;



    void Start()
    {
        anim = GetComponent<Animator>();
        musicSource.clip = bounce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {

            anim.Play("spring");
            musicSource.Play();
        }

    }

    
}
