using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{
    GameObject original;
    // Start is called before the first frame update
    void Start()
    {
        original = gameObject.transform.parent.gameObject;
        gameObject.SetActive(false);
    }

    //responsible for moving the selctor to the given slot.
    public void moveselector(GameObject slot)
    {
        
        if (slot != null)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetParent(slot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,0f);

        }
        else {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(original.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
