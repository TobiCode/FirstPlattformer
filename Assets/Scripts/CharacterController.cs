using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    public float runSpeed;
    public float moveSpeed;
    public float jumpHeight;
    private Animator anim;                  // The animator that controls the characters animations
    public DialogueTrigger dialogueTrigger;
    private bool talkedToCat;
    public bool freeze;

    private SpriteRenderer spriteRenderer;
    private bool grounded;

    //Life
    private bool isDead;
    public int lives;
    public Image live1;
    public Image live2;
    public Image live3;
    private float lastDamage;
    public float againDamageTime;

    public CatController catContoller;

    //UI GameOver
    public GameObject GameOver;
    //Firework
    public GameObject firework;

   

    

    // Use this for initialization
    void Start()
    {
        lives = 3;
        isDead = false;
        talkedToCat = false;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        freeze = false;
    }

    
        //This method is called when the character collides with a collider (could be a platform).
	void OnCollisionEnter2D(Collision2D hit)
	{
		//grounded = true;
    
	}

   
     
    // Update is called once per frame
    void Update()
    {

        if(lives < 1)
        {
            isDead = true;
         
        }

        if(isDead == true)
        {
            //Destroy(gameObject);
            //reload Scene
            //Is Working but make a Nice Scrren with failed etc
            //GameManagerScript.RestartLevel();

            //Show GameOverScreen 
            freeze = true;
            //GameOver.enabled = true;
            GameOver.SetActive(true);
         }

        if(transform.position.y < -120)
        {
            GameOver.SetActive(true);
        }

        


        if (!freeze)
        {
            //update lastTimeDamage
            lastDamage += Time.deltaTime;
            //Jump
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (grounded == true)
                {
                    grounded = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                    //We trigger the Jump animation state
                    anim.SetBool("jumping", true);

                }
            }

            //First check if shift is also clicked -> to run instead of walking
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(runSpeed, GetComponent<Rigidbody2D>().velocity.y);
                if (grounded == true)
                {
                    anim.SetBool("jumping", false);
                    anim.SetFloat("speedRunning", 1);
                }
                if (spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
            }

            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-runSpeed, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetFloat("speed", 1);
                if (grounded == true)
                {
                    anim.SetBool("jumping", false);
                    anim.SetFloat("speedRunning", 1);
                }
                if (spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                }

            }

            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                if (grounded == true)
                {
                    anim.SetBool("jumping", false);
                    anim.SetFloat("speed", 1);
                }
                if (spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
            }


            else if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetFloat("speed", 1);
                if (grounded == true)
                {
                    anim.SetBool("jumping", false);
                }
                if (spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                }


            }

            else
            {
                anim.SetFloat("speed", 0);
                anim.SetFloat("speedRunning", 0);
                if (grounded == true)
                {
                    anim.SetBool("jumping", false);
                }
            }
        }

    }

    //Check if the player is on the Ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Ground")
        {
            grounded = true;
        }

        if (collision.gameObject.tag.ToString() == "Cat")
        {
            if (!talkedToCat)
            {
                freeze = true;
                dialogueTrigger.TriggerDialogue();
                talkedToCat = true;

                //Activate CatController
                catContoller.enabled = true;
            }
        }

        if(collision.gameObject.tag.ToString() == "Goal")
        {
            freeze = true;
            firework.SetActive(true);
        }
       
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Ground")
        {
            grounded = true;
        }
        //loose a live
        if (collision.gameObject.tag.ToString() == "Obstacle")
        {
            if(lastDamage > againDamageTime)
            {
                lastDamage = 0;
                looseLive();
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag.ToString() == "Ground")
        {
            grounded = false;
        }
    }

    public void looseLive()
    {
        
        if(live1.enabled == true && live2.enabled == true && live3.enabled == true)
        {
            live3.enabled = false;
        }
        else if(live1.enabled == true && live2.enabled == true)
        {
            live2.enabled = false;
        }
        else if (live1.enabled == true)
        {
            live1.enabled = false;
        }
        else
        {
            live1.enabled = false;
            live2.enabled = false;
            live3.enabled = false;
        }
        lives = lives - 1;
    }
}

