using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    private GameObject droped;

    public void OnDrop(PointerEventData eventData)
    {
        droped = eventData.pointerDrag;
        Debug.Log(droped.GetComponent<ItemDrag>().getItem().getName());
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
