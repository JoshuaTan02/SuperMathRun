using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    public int speedBoost;
    public int jumpspeed;

    public float speedmultiplier;
    Rigidbody2D rb;

    public Animator anim;

    private bool isGrounded = false;
    private bool isJumping = false;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
        if(isAlive){
        float playerSpeed = 1;
        speedmultiplier = (transform.position.x/150);
        playerSpeed*= (speedBoost+speedmultiplier ) ;
        if(playerSpeed !=0){
            MoveHorizontal(playerSpeed);
        }
        else{
            StopMoving();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        }else{
            //Not alive and end the game. 
            anim.SetInteger("State",-1);
            
            GameController.instance.GameOver();
        
        }

        
    }

    void MoveHorizontal(float speed){
        rb.velocity = new Vector2(speed,rb.velocity.y);
        if(!isJumping){
        anim.SetInteger("State",1);

        }
    }

    void StopMoving(){
        rb.velocity = new Vector2(0,rb.velocity.y);
    }

    void Jump(){
        if(isGrounded){
            rb.AddForce(new Vector2(0,jumpspeed));
            isJumping = true;
            anim.SetInteger("State",2);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
       if( other.gameObject.CompareTag("Ground")){
            Debug.Log("Hit the ground");
            isGrounded = true;
            isJumping = false;            
            
        }else if (other.gameObject.CompareTag("Obstacle")){
            Debug.Log("Hit an obstacle");
            isAlive = false;
        
        }else{

            isGrounded = false;
        }
    }

    public void PausePlayer(){

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("JumpTrigger") && GameController.instance.isCorrectAnswer()){
            Jump();
            GameController.instance.UpdateProblem();
        }
    }
}
