using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    [SerializeField] CraftingLogic CraftingLogic;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Item droppedItem = dropped.GetComponent<ItemDrag>().getItem();
        CraftingLogic.addItem(droppedItem);
        GameObject.Destroy(dropped);



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
