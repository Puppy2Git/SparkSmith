using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionText : MonoBehaviour
{
    private RectTransform rect;
    private Text text;
    public InteractionSphere inter;
    private Vector3 offset = new Vector3(-2f,0.5f);//The size of the thing
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        text = gameObject.transform.GetChild(0).GetComponent<Text>();
        
    }
    public void showThing(bool e) {
        gameObject.SetActive(e);
    }
    public void moveto(Vector3 pos) {
        
        rect.transform.position = pos + offset;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
