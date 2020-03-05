using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;
 

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    Animator anim;
    private bool facingRight = true;

    private int count;
    public Text countText;
    public Text winText;
    private int lives;
    public Text livesText;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        count = 0;
        lives = 3;
        SetCountText();
        SetLivesText();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
    
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            rd2d.velocity = Vector2.up * jumpForce;
        }

    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (isGrounded == false)
        {
            anim.SetInteger("State", 2);
        }

        if (isGrounded == true && hozMovement==0)
        {
            anim.SetInteger("State", 0);
        }

        if (isGrounded == true && hozMovement != 0)
        {
            anim.SetInteger("State", 1);
        }

        if (isGrounded == true && hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            if (count == 4)
            {
                transform.position = new Vector2(55.0f, 62.0f);
                lives = 3;
                SetLivesText();
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count>=8)
        {
            winText.text = "You Win ! Game Created by Hongxu Li !";
            anim.SetInteger("State", 0);
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You Lose ! Game Created by Hongxu Li !";
            anim.SetInteger("State", 3);
            Destroy(this);
            musicSource.Stop();
            musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

}
