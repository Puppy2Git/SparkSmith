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
    private bool canMove;
    private int restrictedMove = 0;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        resetJumps();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Test
        float Horizontal = Input.GetAxis("Horizontal");

        
        //Base movement
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


            body.velocity = new Vector2(Horizontal * speed, body.velocity.y);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            Jumps -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") {
            resetJumps();
        }
    }

    public void resetJumps() {
        Jumps = maxJumps;
    }

    public void moveState(int dir, bool state) {
        
        canMove = state;
        restrictedMove = dir;
    }
}
