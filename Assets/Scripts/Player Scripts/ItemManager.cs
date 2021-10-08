using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Sprite icon;
    private RectTransform rect;
    private Canvas canvas;
    private GameObject slot;
    private Vector2 offset = new Vector2(930,-440);
    private bool beingDragged = false;
    private GameObject target;
    public void setIcon(Sprite icon) {
        if (icon == null)
        {
            this.icon = icon;
            rect = gameObject.GetComponent<RectTransform>();
            //UGLY
            canvas = gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<Canvas>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        slot = gameObject.transform.parent.gameObject;
        setIcon(null);
    }
    public void resetPosition() {
        transform.SetParent(slot.transform, false);
        rect.anchoredPosition = new Vector2(0, 0);
        beingDragged = false;
    }
    
    public void OnEndDrag(PointerEventData eventData) {
        if (target != null)
        {
            Debug.Log("Changing parent.");
            slot = target;
        }
        transform.SetParent(slot.transform, false);
        rect.anchoredPosition = new Vector2(0, 0);
        beingDragged = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        target = null;
        if (beingDragged)
        {
            if (collision.gameObject != slot)
            {
                if (collision.tag == "Slot" && collision.gameObject.transform.childCount == 0)
                {
                    Debug.Log("Is valid");
                    target = collision.gameObject;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(gameObject.transform.parent.parent.parent.transform,false);
        rect.anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition - offset;
        transform.SetAsLastSibling();
        beingDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
        
    }

    // Update is called once per frame
    
}
