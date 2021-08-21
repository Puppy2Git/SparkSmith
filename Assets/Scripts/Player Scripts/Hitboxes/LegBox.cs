using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBox : MonoBehaviour
{
    private CharacterMovement player;
    // Start is called before the first frame update
    void Awake()
    {
        //Gets player script object
        player = transform.parent.parent.GetComponent<CharacterMovement>();
        Physics2D.IgnoreCollision(transform.parent.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When touching ground
    private void OnTriggerEnter2D(Collider2D col)
    {
        {
            if (col.gameObject.tag == "Ground")
            {
                player.resetJumps();
            }
        }
    }
}
