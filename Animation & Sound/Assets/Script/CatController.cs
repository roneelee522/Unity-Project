using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Animator anim;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    // The code above this comment creates an Animator variable called "anim" so that we can do things with it later; the code below it in the Start function sets the value of our anim variable using our Animator component in Unity

    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // The code below here sets the Integer value of our anim variable based on whether we have the W key pressed down or not. If it is pressed down, the State integer gets set to 1; when it is released, we set it back to 0.  We do something similar for the running animation with the Left Shift key.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            musicSource.Stop();
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            musicSource.Stop();
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            musicSource.loop = true;
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            musicSource.loop = false;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
