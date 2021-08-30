using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScript : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float bulletSpeed = 10;
    public Rigidbody2D bullet;
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



    void Fire()
    {
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.up * bulletSpeed;
    }
}

