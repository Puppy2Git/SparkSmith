using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class rotateScript : MonoBehaviour
{
    public GameObject player; //For player position
    public float offset; //offset for debugging
    public float bulletSpeed = 25; // Setting bullet speed
    public BulletScript bullet; //Grabbing bullet object
    public Transform Crosshair; // Crosshair location
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //Grabs mouse position and sets it in world position in relation to the main camera
        Vector3 mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        
        //determine rotation based off of position
        Vector2 direction = new Vector2(mousepos.x - transform.position.x, mousepos.y - transform.position.y);
        transform.position = player.transform.position + new Vector3(0, offset);
        transform.up = direction;
        
        //If the character fires (Should be moved to a character attack script)
        
    }

    private float getMouseX() {
        Vector3 mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        return mousepos.x;
    }

    private float getMouseY() {
        Vector3 mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        return mousepos.y;
    }



    public void Fire()
    {
        //Generates a new bullet
        BulletScript bulletClone = (BulletScript)Instantiate(bullet, Crosshair.transform.position, transform.rotation);
        //Sets it's direction and speed
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        //Tells it is is a lie
        bulletClone.dupe = true;
    }
}

