using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialBadDrop : MonoBehaviour, IDropHandler
{
    private TutorialCraftingLogic tutorialCrafting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialCrafting = GetComponentInParent<TutorialCraftingLogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Item droppedItem = dropped.GetComponent<ItemDrag>().getItem();
        tutorialCrafting.createItem(droppedItem);
        GameObject.Destroy(dropped);
    }

}
