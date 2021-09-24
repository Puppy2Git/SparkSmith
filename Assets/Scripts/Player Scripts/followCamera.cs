using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Alexander Garcia
public class followCamera : MonoBehaviour
{

    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float cameraOffset = 1f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(desiredPos.x, desiredPos.y + cameraOffset, transform.position.z), smoothSpeed);
        transform.position = smoothPos;
    }
}
