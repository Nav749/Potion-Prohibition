using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialInevtorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] bool isChecker = false;
    [SerializeField] TutorialPotionCollection potionCollection;
    private bool internalbool = true;
    public bool isCorrect;

    public void Update()
    {
        if (isChecker)
        {
            if (this.transform.childCount != 0 && internalbool)
            {
                InventoryItem potionToCheck = this.transform.GetChild(0).gameObject.transform.GetComponent<InventoryItem>();
                Debug.Log(potionToCheck.potionName);
                isCorrect = potionCollection.CheckPotion(potionToCheck.potion);
                Debug.Log(isCorrect.ToString());
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
