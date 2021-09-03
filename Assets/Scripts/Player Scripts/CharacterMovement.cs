using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private int Jumps;
    public int maxJumps;
    public float speed = 5f;
    public float jumpForce;
    public float dashForce;
    private float dashTimer;
    public float dashDelay;
    public float dashDuration;
    private float dashCooldown;
    private bool canMove;
    private bool canDash = true;
    private bool isDashing;
    private int restrictedMove = 0;


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        resetJumps();
        canMove = true;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        float Horizontal = Input.GetAxis("Horizontal");

        
        //If and switch statement to determine if the character can move and if the movement is restricted.
        if (canMove)
        {
            switch (restrictedMove) {
                //Normal
                case 0:
                    break;
                //Can't move left
                case 1:
                    if (Horizontal < 0f)
                    {
                        Horizontal = 0;
                    }
                    break;
                //Can't move right
                case 2:
                    if (Horizontal > 0f)
                    {
                        Horizontal = 0;
                    }
                    break;

            }

            //Changing Horizontal Velocity
            body.velocity = new Vector2(Horizontal * speed, body.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0)
            {
                //Changing Vertical Velocity
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                Jumps -= 1;

            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && canDash) {//Checks if the character is dashing or not and if pressing the dash key
                moveState(false); //Disable movement
                body.gravityScale = 0f; //Ignores Gravity
                isDashing = true;
                canDash = false;
                dashTimer = Time.time + dashDuration;

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

    public void directionRestrict(int dir)
    {
        restrictedMove = dir;
    }

    //This handles Dashing it does not check the conditions for dashing
    public void DashDelays() {
        if (dashCooldown <= Time.time && !isDashing && !canDash) {
            canDash = true;
        }
        
    }
    //This handles the active dash velocity
    public void Dash() {
        if (isDashing)
        {

            body.velocity = new Vector2(dashForce, 0);
            if (dashTimer <= Time.time)
            {
                body.gravityScale = 1f;
                moveState(true);
                isDashing = false;
                dashCooldown = Time.time + dashDelay;
            }

        }
    }
}
