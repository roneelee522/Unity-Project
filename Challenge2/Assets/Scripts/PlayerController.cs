using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;
    public Vector2 velocity;
    private CapsuleCollider2D col;
    private CircleCollider2D scol;
    public Vector2 slidev;
    private bool isRunning;
    private bool isSpacing;
 

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
    public AudioSource slide;
    public AudioSource crow;
    public AudioSource coin;
    public AudioSource musicSource;

    bool sliding = false;
    float slideTimer = 0f;
    public float maxSlideTime;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        scol = GetComponent<CircleCollider2D>();
        scol.enabled = false;
        isGrounded = true;
        isSpacing = true;
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
            isRunning = false;
        }

        if (isGrounded == true && hozMovement==0)
        {
            anim.SetInteger("State", 0);
            isRunning = false;
        }

        if (isGrounded == true && hozMovement != 0)
        {
            anim.SetInteger("State", 1);
            isRunning = true;
        }

        if (isGrounded == true && hozMovement == 0)
        {
            anim.SetInteger("State", 0);
            isRunning = false;
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (isGrounded == true && isRunning ==true && Input.GetKeyDown(KeyCode.Space)&&isSpacing==true)
        {
            isSpacing = false;
            slideTimer = 0f;
            anim.SetBool("isSliding", true);
            scol.enabled = true;
            col.enabled = false;
            sliding = true;
            if (sliding)
            {
                slide.Play();
            }
            if (facingRight)
            { rd2d.velocity = slidev; }
            else
            {
                rd2d.velocity = -slidev;
            }

        }
        if(sliding || Input.GetKeyUp(KeyCode.Space))
        {
            //GameManager.IsInputEnabled = false;
            slideTimer += Time.deltaTime;
            if(slideTimer>maxSlideTime)
            {

                isSpacing = true;
                sliding = false;
                anim.SetBool("isSliding", false);
                col.enabled = true;
                scol.enabled = false;
                isRunning = false;
            }
        }


    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            coin.Play();
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
            crow.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Spring")
        {
            rd2d.velocity = Vector2.zero;
            rd2d.velocity = velocity;
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
            anim.Play("dead");
            Destroy(this);
            musicSource.Stop();
            musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

}
