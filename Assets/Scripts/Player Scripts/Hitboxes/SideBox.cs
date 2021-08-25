using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBox : MonoBehaviour
{
    private CharacterMovement move;
    // Start is called before the first frame update
    void Awake()
    {
        //Gets player script object
        move = transform.parent.parent.GetComponent<CharacterMovement>();
        //Ignores Character hitbox and feet should be moved to hitboxes for better organization
        Physics2D.IgnoreCollision(transform.parent.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(transform.parent.Find("Leg Hitbox").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground") {
            Debug.Log("In col");
            //To the right
            if (col.gameObject.transform.position.x > transform.parent.parent.position.x) 
            {
                move.moveState(2, true);
            }
            else if (col.gameObject.transform.position.x < transform.parent.parent.position.x)
            {
                move.moveState(1, true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Debug.Log("out col");
            move.moveState(0, true);
        }
    }
}
