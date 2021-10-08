using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHolderScript : MonoBehaviour
{
    private List<GameObject> allslots;
    public GameObject slotTemplate;
    // Start is called before the first frame update
    void Start()
    {
        allslots = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < 15; i++) {
            tmp = Instantiate(slotTemplate);
            tmp.transform.SetParent(gameObject.transform);
            tmp.tag = "Slot";
            tmp.transform.localScale = new Vector3(1, 1, 1);
            allslots.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
