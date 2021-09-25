using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class CharacterMovement : MonoBehaviour
{
    //onject
    private Rigidbody2D body; //Grabs Rigidbody

    //Handles Jumps
    private int Jumps; //jumps that the character has
    private int maxJumps = 3; //The maximum jumps they have
    public float jumpForce = 1.5f;//The ammout of force applied while jumping

    
    //Handles Dash
    public float dashForce = 25f;//The ammount of force applied while dashing
    private float dashTimer;//Internal dash timer
    private float dashDelay = 2f;//The ammount of time before another dash can be used
    private float dashDuration = 0.25f;//The length of the dash
    private float dashCooldown;//Internal dash cooldown timer
    private bool canDash;//If the player can dash
    private bool isDashing;//Internal is the player dashing

    //Handles Movement
    public float speed = 5f;//The speed at which they move
    private bool canMove;//Whether the player can move
    private float dashDir;//The direction the dash is heading
    private float gravity;//gravity nooooo
    public float gravityConstant = -20f;//The inital gravity
    private float Horizontal;   
    //Called by playerController
    public void Dash() {
        if (!isDashing && canDash)
        {
            moveState(false); //Disable movement
            gravity = 0f; //Ignores Gravity
            isDashing = true;
            canDash = false;
            dashTimer = 0f;
        }
    }


    void Awake()
    {
        //Disabling and setting constants
        body = GetComponent<Rigidbody2D>();
        resetJumps();
        canMove = true;
        canDash = true;
        gravity = gravityConstant;
        body.gravityScale = 0f;
        dashDir = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();//handles movement
        DashMovement();//Handles Dashing
        DashDelays();//Handles Dash Delay calculations
    }

    public void setHorizontal(float hori) {
        Horizontal = hori;
    }

    public void movement() {
        //If and statement to determine if the character can move.
        if (canMove)
        {

            //Changing Horizontal Velocity



            //If the character can jump, maybe move to own function?

            //Changing velocity by gravity.
            //if (!isground)
            //{
            
            //if (!isground)
            //{
                body.velocity = new Vector2(body.velocity.x, body.velocity.y + (gravity * Time.deltaTime));

            //}
            
                body.velocity = new Vector2(Horizontal * speed * Time.deltaTime, body.velocity.y);
            
            //}



            //Determines dash direction
            if (!isDashing)
            {
                if (Horizontal > 0)
                {
                    dashDir = 1f;
                }
                else if (Horizontal < 0)
                {
                    dashDir = -1f;
                }
            }

        }

    }

    public void Jump() {
        if (Jumps > 0) {
            //isground = false;
            body.velocity = new Vector2(body.velocity.x, Mathf.Sqrt(jumpForce * -3f * gravity * Time.deltaTime));
            Jumps -= 1;//Decressing jumps
        }
    }

    //Resets the Jumps
    public void resetJumps() {
        //isground = true;
        Jumps = maxJumps;
    }


    //This sets the canMove and the restrictedMove booleans
    public void moveState(bool state) {
        
        canMove = state;
    }


    //This handles Dashing it does not check the conditions for dashing
    public void DashDelays() {
        dashCooldown += Time.deltaTime;
        if (dashCooldown >= dashDelay) {
            canDash = true;
        }
        
    }
    //This handles the active dash velocity
    public void DashMovement() {
        if (isDashing)
        {
            canDash = false;
            dashTimer += Time.deltaTime;
            body.velocity = new Vector2(dashForce * dashDir * Time.deltaTime, 0);
            if (dashTimer >= dashDuration)
            {
                dashCooldown = 0f;
                gravity = gravityConstant;
                moveState(true);

                isDashing = false;
                
            }

        }
    
    }

  

}
