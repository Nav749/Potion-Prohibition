using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class CraftingLogic : MonoBehaviour
{
    [SerializeField] private AudioClip sucsessfulSound;
    [SerializeField] private AudioClip unsucsessfulSound;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        hideSlots();
        potionDisplay.enabled = false;
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

    public void createItems()
    {
        for (int i = 0; i < gameManager.inventory.Length; i++)
        {

            for (int j = 0; j < gameManager.inventory[i].getAmount(); j++)
            {
                createItem(gameManager.inventory[i]);
            }
        }
        
    }

    public void distroyItems() {
        ItemDrag[] distructable = parent.GetComponentsInChildren<ItemDrag>();
        foreach (ItemDrag item in distructable) { 
            GameObject.Destroy(item.gameObject);
        }

    }



    #endregion

    #region crafting item management
    int count = 0;
    List<Item> droppedItems = new List<Item>();
    Item spice = null;
    Item rocks = null;

    [SerializeField] Image[] slots = new Image[3];
    [SerializeField] Image rockSlot;
    [SerializeField] Image spiceSlot;

    public void addItem(Item item)
    {
        if (item.getName() == "Rocks")
        {
            if (rocks == null)
            {
                rocks = item;
                rockSlot.sprite = item.getImage();
                rockSlot.enabled = true;
            }
            else
            {
                createItem(item);
            }
        }
        else if (item.getName() == "Spices")
        {
            if (spice == null)
            {
                spice = item;
                spiceSlot.sprite = item.getImage();
                spiceSlot.enabled = true;
            }
            else
            {
                createItem(item);
            }
        }
        else
        {
            if (count < 3)
            {
                droppedItems.Add(item);
                slots[count].sprite = item.getImage();
                slots[count].enabled = true;
                count++;
            }
            else
            {
                createItem(item);
            }
        }
    }

    private void hideSlots() { 
        rockSlot.enabled = false;
        spiceSlot.enabled = false;
        for (int i = 0; i < slots.Length; i++) {
            slots[i].enabled = false;
        }
    }

    public void clearPot()
    {
        if (spice != null)
        {
            createItem(spice);
            

        }
        if (rocks != null)
        {
            createItem(rocks);

        }
        for (int i = 0; i < droppedItems.Count; i++)
        {
            if (droppedItems[i] != null)
            {
                createItem(droppedItems[i]);
            }
        }
        droppedItems.Clear();
        clearSlots();
        //hideSlots();
        count = 0;
    }

    private void clearSlots() { 
        spiceSlot.sprite = null;
        spiceSlot.enabled = false;
        spice = null;
        rockSlot.sprite = null;
        rockSlot.enabled = false;
        rocks = null;
        for (int i = 0; i < slots.Length; i++) { 
            slots[i].sprite = null;
            slots[i].enabled = false;
        }
        count = 0;
    }
    
    #endregion

    #region brewing

    [SerializeField] private Potion[] potions;
    [SerializeField] private Image potionDisplay;
    
    public void brew()
    {
        bool sucessful = false;

        for (int i = 0; i < potions.Length; i++) {
            
            
            if (potions[i].checkIngredients(droppedItems))
            { 
                Potion temp = potions[i].CloneViaFakeSerialization();

                if (spice != null)
                {
                    temp.setSpiced(true);
                }
                if (rocks != null)
                {
                    temp.setOnTheRocks(true);
                }


                foreach (Item item in droppedItems) { 
                    item.decrementAmount();
                }


                clearSlots();
                droppedItems.Clear();
                potionDisplay.sprite = temp.getImage();
                potionDisplay.enabled = true;
                
                audioSource.clip = sucsessfulSound;
                audioSource.Play();
                
                gameManager.potions.Add(temp);
                sucessful = true;

                Invoke("clearPotion", 3);

                break;
            }
        }


        if (!sucessful) { 
            //unsucsessfulSound.Play();
        }



    }


    private void clearPotion() {
        potionDisplay.enabled = false;
    }

    #endregion


}
