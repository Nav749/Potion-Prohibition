using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler
{
    
    private GameObject dropped;
    [SerializeField] CraftingLogic craftingLogic;

    public void OnDrop(PointerEventData eventData)
    {
        dropped = eventData.pointerDrag;
        craftingLogic.addItem(dropped.GetComponent<Item>());
        
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
