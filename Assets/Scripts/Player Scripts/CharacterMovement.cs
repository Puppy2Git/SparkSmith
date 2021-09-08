using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D body; //Grabs Rigidbody
    private int Jumps; //jumps that the character has
    public int maxJumps; //The maximum jumps they have
    public float speed = 5f;//The speed at which they move
    public float jumpForce;//The ammout of force applied while jumping
    public float dashForce;//The ammount of force applied while dashing
    private float dashTimer;//Internal dash timer
    public float dashDelay;//The ammount of time before another dash can be used
    public float dashDuration;//The length of the dash
    private float dashCooldown;//Internal dash cooldown timer
    private bool canMove;//Whether the player can move
    private bool canDash;//If the player can dash
    private bool isDashing;//Internal is the player dashing
    private float dashDir;//The direction the dash is heading
    private float gravity;//gravity nooooo
    public float gravityConstant;//The inital gravity
    // Start is called before the first frame update
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
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");

        
        //If and statement to determine if the character can move.
        if (canMove)
        {
            
            //Changing Horizontal Velocity
            body.velocity = new Vector2(Horizontal * speed, body.velocity.y);


            //If the character can jump, maybe move to own function?
            if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0)
            {
                //Changing Vertical Velocity
                body.velocity = new Vector2(body.velocity.x, Mathf.Sqrt(jumpForce * -3f * gravity));
                Jumps -= 1;//Decressing jumps
            }
            //Changing velocity by gravity.
            body.velocity = new Vector2(body.velocity.x, body.velocity.y + (gravity * Time.deltaTime));
            
            
            
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
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && canDash) {//Checks if the character is dashing or not and if pressing the dash key
                moveState(false); //Disable movement
                gravity = 0f; //Ignores Gravity
                isDashing = true;
                canDash = false;
                dashTimer = 0f;
                

            }

        }
        
        //Dash movement
        Dash();//Handles Dashing
        DashDelays();//Handles Dash Delay calculations
    }

    

    //Resets the Jumps
    public void resetJumps() {
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
    public void Dash() {
        if (isDashing)
        {
            canDash = false;
            dashTimer += Time.deltaTime;
            body.velocity = new Vector2(dashForce * dashDir, 0);
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
