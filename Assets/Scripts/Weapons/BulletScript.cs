using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class BulletScript : MonoBehaviour
{

    public float cleanupDelay;//How long the bullet should last if it fails to hit its target
    private float cleanupTimer;//Internal timer
    //Whether the bullet is the main or a copy
    // Start is called before the first frame update
    void Start()
    {
        cleanupTimer = 0f;//Starts it
    }

    // Update is called once per frame
    void Update()
    {
        cleanupTimer += Time.deltaTime;//Use the timer
        
        if (cleanupTimer >= cleanupDelay) {
            cleanupTimer = 0;
            gameObject.SetActive(false);//Come here cupcake!
        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")//d i r t
        {
            cleanupTimer = 0f;
            gameObject.SetActive(false);
            
        }
    }
}
