using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class BulletScript : MonoBehaviour
{

    public float cleanupDelay;//How long the bullet should last if it fails to hit its target
    private float cleanupTimer;//Internal timer
    public bool dupe = false;//Whether the bullet is the main or a copy
    // Start is called before the first frame update
    void Start()
    {
        cleanupTimer = 0f;//Starts it
    }

    // Update is called once per frame
    void Update()
    {
        if (dupe)//If it is a dupe
        {
            cleanupTimer += Time.deltaTime;//Use the timer
        }
        if (cleanupTimer >= cleanupDelay) {
            Destroy(gameObject);//Come here cupcake!
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && dupe)//d i r t
        {
            Destroy(gameObject);//BONK!
        }
    }
}
