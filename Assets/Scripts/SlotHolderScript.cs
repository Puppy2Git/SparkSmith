using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHolderScript : MonoBehaviour
{
    private List<GameObject> allslots;
    public GameObject slotTemplate;
    public InventoryManagement invi;
    // Start is called before the first frame update
    void Start()
    {
        allslots = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < 16; i++) {
            tmp = Instantiate(slotTemplate);
            tmp.transform.SetParent(gameObject.transform);
            tmp.tag = "Slot";
            tmp.GetComponent<SlotManager>().position = i;
            tmp.transform.localScale = new Vector3(1, 1, 1);
            allslots.Add(tmp);
        }
        invi.initInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
