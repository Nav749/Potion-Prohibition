using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    [SerializeField] bool isChecker = false;
    private bool internalbool = true;

    public void Update()
    {
        if (isChecker)
        {
            if (this.transform.childCount != 0 && internalbool)
            {
                InventoryItem potionToCheck = this.transform.GetChild(0).gameObject.transform.GetComponent<InventoryItem>();
                Debug.Log(potionToCheck.potionName);
                GameManager.Instance.OrdertoCheck = potionToCheck.getPotion();
                internalbool = false;
            }
            else if (this.transform.childCount == 0)
            {
                internalbool = true;
                GameManager.Instance.OrdertoCheck = null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
