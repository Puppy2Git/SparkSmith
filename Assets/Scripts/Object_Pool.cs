using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool : MonoBehaviour
{
    public static Object_Pool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public GameObject BulletPoolObject;
    public int ammounttopool;

    private void Awake()
    {
        SharedInstance = this;

    }
    //When the game Starts
    private void Start()
    {
        //Create a new list object
        pooledObjects = new List<GameObject>();
        GameObject tmp;//Create a temp object
        for (int i = 0; i < ammounttopool; i++) {
            tmp = Instantiate(objectToPool);//New bullet
            tmp.SetActive(false);//Not active
            pooledObjects.Add(tmp);//Add to pool
            tmp.transform.parent = BulletPoolObject.transform;//Move under object so Hierarchy is not a pain in the
        }
    }
    //Get a pooled object
    public GameObject GetPooledObject() {
        for (int i = 0; i < ammounttopool; i++) {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
