using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler
{

    [SerializeField] private Item item;
    
    private Image image;
    private RectTransform rectTransform;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        image = GetComponent<Image>();
        image.sprite = item.getImage();
        
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public Item getItem() 
    { 
        return item; 
    }

    public void setItem(Item item)
    {
        this.item = item;
    }
}
