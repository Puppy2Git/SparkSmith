using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class LegBox : MonoBehaviour
{
    public CharacterMovement player;
    // Start is called before the first frame update
    void Awake()
    {
        //Gets player script object
        
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
