using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
	Rigidbody2D rb;
    private LevelManager Lman;
    Animator playerAnim;
    bool canBeTapped;


    public AudioSource DeadSound;



    void Start(){
        if (PlayerPrefs.GetInt("CheatMode", 0) == 0)
            canBeTapped = false;
        else
            canBeTapped = true;
    	rb = GetComponent<Rigidbody2D>();
        Lman = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){

    	if(Input.GetMouseButtonDown(0) && canBeTapped){
            if (!EventSystem.current.IsPointerOverGameObject())
                Tapped();
    	}
        if (transform.position.x != -13)
            transform.position = new Vector3(-13f,transform.position.y);
    }
    void FixedUpdate()
    {

    }

    void Tapped(){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>().Shake();
        playerAnim.SetBool("IsGrounded", false);
        if (rb.gravityScale == 1){

                rb.AddForce(new Vector2(0, 15.5f), ForceMode2D.Impulse);
                rb.gravityScale = -1f;
	    		transform.localScale = new Vector3(0.65f,-0.65f,0.65f);
	    	}
    	else{
                rb.AddForce(new Vector2(0, -15.5f), ForceMode2D.Impulse);
                rb.gravityScale = 1f;
	    		transform.localScale = new Vector3(0.65f,0.65f,0.65f);
	    	}
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor")
        {
            canBeTapped = true;
            playerAnim.SetBool("IsGrounded", true);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane" || other.tag == "Obstacles")
        {
            playerAnim.SetBool("IsDead", true);
            DeadSound.Play();

            Lman.GameOver();
      
        }
    }
}
