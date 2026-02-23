using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region item creation
    [SerializeField] private GameObject dragablePrefab;
    [SerializeField] private RectTransform parent;

    public void createItem(Item item)
    {
        //the |abs| uper and lower range of where Items can spawn
        float widthRange = (parent.rect.width - dragablePrefab.GetComponent<RectTransform>().rect.width) / 2;
        float HeightRange = (parent.rect.height - dragablePrefab.GetComponent<RectTransform>().rect.height) / 2;


        //random point within those ranges
        float width = Random.Range(-widthRange, widthRange);
        float height = Random.Range(-HeightRange, HeightRange);

        //creates the now game object
        dragablePrefab.GetComponent<ItemDrag>().setItem(item);
        GameObject temp = Instantiate(dragablePrefab, parent);
        temp.transform.localPosition = new Vector3(width, height, 0);
    }

    public void createItems() {
        for (int i = 0; i <= gameManager.inventory.Length; i++)
        {

            for (int j = 0; j < gameManager.inventory[i].getAmount(); j++) 
            {
                createItem(gameManager.inventory[i]);
            }
        }
        //gameManager.clearInventory();
    }



    #endregion

    #region crafting item management
    int count = 0;
    Item[] droppedItems = new Item[5];
    int spicesIdx = 3;
    int rocksIdx = 4;

    [SerializeField] Image[] slots = new Image[5];
    public void addItem(Item item) {
        Debug.Log(item.getName());
        if (item.getName() == "Rocks")
        {
            if (droppedItems[rocksIdx] == null){
                droppedItems[rocksIdx] = item;
                slots[rocksIdx].sprite = item.getImage();
            }
            else
            {
                createItem(item);
            }
        }
        else if (item.getName() == "Spices")
        {
            if (droppedItems[spicesIdx] == null)
            {
                droppedItems[spicesIdx] = item;
                slots[spicesIdx].sprite = item.getImage();
            }
            else {
                createItem(item);
            }
        }
        else {
            if (count < 3)
            {
                droppedItems[count] = item;
                slots[count].sprite = item.getImage();
            }
            else
            {
                createItem(item);
            }
        }
    }

    public void clearPot() {
        for (int i = 0; i < droppedItems.Length; i++) {
            if (droppedItems[i] != null) { 
                createItem(droppedItems[i]); 
                droppedItems[i] = null;
            }
        }
        count = 0;
    }

    #endregion

}
