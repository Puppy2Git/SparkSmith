using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScript : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float bulletSpeed = 25;
    public float bulletOffset = 1.25f;
    public Rigidbody2D bullet;
    public Transform Crosshair;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);

        Vector2 direction = new Vector2(mousepos.x - transform.position.x, mousepos.y - transform.position.y);
        transform.position = player.transform.position + new Vector3(0, offset);
        transform.up = direction;
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    public float getMouseX() {
        Vector3 mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        return mousepos.x;
    }

    public float getMouseY() {
        Vector3 mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        return mousepos.y;
    }



    void Fire()
    {
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, Crosshair.transform.position, transform.rotation);
        bulletClone.velocity = transform.up * bulletSpeed;
    }
}

