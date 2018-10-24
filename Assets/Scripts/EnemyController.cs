using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour {

    public GameObject player;

    public float speed;

    private bool wallHit;
    private bool playerHit;
    public Transform wallHitBox;
    public float wallHitWidth;
    public float wallHitHeight;
    public LayerMask isGround;


    private bool headHit;
    public Transform headHitBox;
    public float headHitWidth;
    public float headHitHeight;


    private AudioSource source;
    public AudioClip goombaDeath;
    public AudioClip marioDeath;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    public Text loseText;


    // Use this for initialization
    void Start () {

        startLoseText();

    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;
        }

        headHit = Physics2D.OverlapBox(headHitBox.position, new Vector2(headHitWidth, headHitHeight), 0);

        if (headHit == true)
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(goombaDeath, transform.position);
        }

        playerHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0);

        if(playerHit == true)
        {
            player.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.gameObject.CompareTag("Player")) && (headHit== false))
        {
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(marioDeath, transform.position);
            SetLoseText();
        }

        

    }

    void startLoseText()
    {
        loseText.text = "";
    }


    void SetLoseText()
    {
        loseText.text = "YOU LOSE";
    }

}

