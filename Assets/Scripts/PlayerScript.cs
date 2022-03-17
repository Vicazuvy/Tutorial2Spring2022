using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;

    public float jumpForce;
    public Text score;
    public Text lives;
    public GameObject winTextObject;
    public GameObject loseTextObject;

  //  public Text lives;
    private int scoreValue = 0;

    private int livesValue = 3;

    private bool isOnGround;

    private bool facingRight = true;
public Transform groundcheck;
public float checkRadius;
public LayerMask allGround;

public AudioClip musicClipOne;
public AudioClip musicClipTwo;
public AudioSource musicSource;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
loseTextObject.SetActive(false);
           winTextObject.SetActive(false);
        score.text = "Score: " + scoreValue.ToString();
        anim = GetComponent<Animator>();
        lives.text = "Lives: " + livesValue.ToString();    
      
    }

    // Update is called once per frame

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
      isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

      if(vertMovement > 0 && isOnGround == false)
      {
          anim.SetInteger("State", 2);
      }
       else if(hozMovement != 0 && isOnGround == true)
      {
        anim.SetInteger("State", 1);
      }
      else if(vertMovement == 0 && isOnGround == true)
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

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if(collision.collider.tag == "Coin")
       {
           scoreValue += 1;
           score.text = "Score: " + scoreValue.ToString();
           Destroy(collision.collider.gameObject);

           if(scoreValue == 4)
       {
           transform.position = new Vector2(65, 2);
           livesValue = 3;
           lives.text = "Lives: " + livesValue.ToString();
       }
          if(scoreValue == 8)
          {
            winTextObject.SetActive(true);
           musicSource.clip = musicClipTwo;
           musicSource.Play();
           musicSource.loop = false;
          }
       }
       if(collision.collider.tag == "Enemy")
       {
          livesValue -= 1;
          lives.text = "Lives: " + livesValue.ToString();
          Destroy(collision.collider.gameObject);

          if(livesValue == 0 && scoreValue != 8){
               loseTextObject.SetActive(true);
               musicSource.loop = false;
                anim.SetInteger("State", 0);
               Destroy(this);
          }
       }
       
   }


   private void OnCollisionStay2D(Collision2D collision)
   {
       if(collision.collider.tag == "Ground" && isOnGround)
       {
           if(Input.GetKey(KeyCode.W)) //add down?
           {
               rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
               anim.SetInteger("State", 2);
           }

       }
       
   }

    void Update()
    {

     //   if(Input.GetKeyDown(KeyCode.LeftArrow))
      //  {
        //    anim.SetInteger("State", 1);
      //  }

      //  if(Input.GetKeyUp(KeyCode.LeftArrow))
      //  {
         //   anim.SetInteger("State", 0);
       // }

        //  if(Input.GetKeyDown(KeyCode.RightArrow))
      //  {
          //  anim.SetInteger("State", 1);
      //  }

        //  if(Input.GetKeyUp(KeyCode.RightArrow))
       // {
         //   anim.SetInteger("State", 0);
      //  }

       // if(Input.GetKeyDown(KeyCode.UpArrow))
      //  {
      //      anim.SetInteger("State", 2);
      // }

        if (Input.GetKey("escape"))
           {
             Application.Quit();
           }
    }
}
