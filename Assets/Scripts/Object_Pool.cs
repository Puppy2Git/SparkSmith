using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool : MonoBehaviour
{
    public static Object_Pool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int ammounttopool;

    private void Awake()
    {
        SharedInstance = this;

    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < ammounttopool; i++) {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
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
