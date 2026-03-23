using UnityEngine;
using UnityEngine.EventSystems;

public class badDrop : MonoBehaviour, IDropHandler
{

    private CraftingLogic CraftingLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CraftingLogic = GetComponentInParent<CraftingLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Item droppedItem = dropped.GetComponent<ItemDrag>().getItem();
        CraftingLogic.createItem(droppedItem);
        GameObject.Destroy(dropped);
    }

}
