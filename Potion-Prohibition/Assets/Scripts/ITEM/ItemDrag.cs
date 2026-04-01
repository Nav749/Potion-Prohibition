using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        transform.position = Input.mousePosition;
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
