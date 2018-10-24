using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public float speed;
    public float jumpforce;

    public Text scoreText;

    public int amountofCoins;

    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;


    private bool isOnGround;
    private bool isWalking;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    private int score;
    public Transform mushroom;

    private Animator anim;

    // private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;





    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score = 0;
        SetScoreText();

    }

    void Awake()
    {

        source = GetComponent<AudioSource>();


    }

    private void Update()
    {

        



    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
            

        float moveHorizontal = Input.GetAxis("Horizontal");


        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);

        if (isOnGround == false)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsJumping", true);

            


        }
        
        else if ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.RightArrow)) && isOnGround == true))
        {
            anim.SetBool("IsWalking", true);  
            
        }

        else
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsWalking", false);
        }







        //stuff I added to flip my character
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score = score + 10;
            SetScoreText();
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
        }

        if ((other.gameObject.CompareTag("CoinBox")) && (amountofCoins > 0))
        {
            amountofCoins = amountofCoins - 1;
            score = score + 10;
            SetScoreText();
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
        }

        if (other.gameObject.CompareTag("MushroomBox"))
        {
            other.gameObject.SetActive(false);
            score = score + 10;
            SetScoreText();
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
        }

        if (other.gameObject.CompareTag("MushroomBox"))
        {
            other.gameObject.SetActive(false);
            score = score + 10;
            SetScoreText();
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetScoreText()
    {

        scoreText.text = "SCORE: " + score.ToString();
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKey(KeyCode.UpArrow))
            {
                // rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpforce;


                float vol = Random.Range(volLowRange, volHighRange);
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);



            }
        }
    }
}
