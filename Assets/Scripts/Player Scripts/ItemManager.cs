using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Sprite icon;
    private RectTransform rect;
    private Canvas canvas;
    
    public void setIcon(Sprite icon) {
        if (icon == null)
        {
            this.icon = icon;
            rect = gameObject.GetComponent<RectTransform>();
            //UGLY
            canvas = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        setIcon(null);
        Debug.Log("Hello World");
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("End Drag");

    }
    

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragg");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Update is called once per frame
    
}
