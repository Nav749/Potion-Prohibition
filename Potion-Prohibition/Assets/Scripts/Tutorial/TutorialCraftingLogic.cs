using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCraftingLogic : MonoBehaviour
{
    [SerializeField] private AudioClip sucsessfulSound;
    [SerializeField] private AudioClip unsucsessfulSound;
    [SerializeField] private TutorialInventory inventory;
    [SerializeField] private TutorialPotionCollection potionCollection;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (bookOpen)
            {
                closeBook();
            }
            else if (!bookOpen)
            {
                openBook();
            }
        }
    }

    private void Awake()
    {
        hideSlots();
        potionDisplay.enabled = false;
        closeBook();
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
        for (int i = 0; i < inventory.inventory.Length; i++)
        {

            for (int j = 0; j < inventory.inventory[i].getAmount(); j++)
            {
                createItem(inventory.inventory[i]);
            }
        }

    }

    public void distroyItems()
    {
        ItemDrag[] distructable = parent.GetComponentsInChildren<ItemDrag>();
        foreach (ItemDrag item in distructable)
        {
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

    private void hideSlots()
    {
        rockSlot.enabled = false;
        spiceSlot.enabled = false;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].enabled = false;
        }
    }

    public void clearPot()
    {
        if (spice != null)
        {
            createItem(spice);
            spice = null;
            spiceSlot.sprite = null;
        }
        if (rocks != null)
        {
            createItem(rocks);
            rocks = null;
            rockSlot.sprite = null;

        }
        for (int i = 0; i < droppedItems.Count; i++)
        {
            if (droppedItems[i] != null)
            {
                createItem(droppedItems[i]);
                slots[i].sprite = null;
            }
        }
        droppedItems.Clear();
        hideSlots();
        count = 0;
    }

    private void clearSlots()
    {
        spiceSlot.sprite = null;
        rockSlot.sprite = null;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = null;
        }
    }

    #endregion

    #region brewing

    [SerializeField] private Potion[] potions;
    [SerializeField] private Image potionDisplay;

    public void brew()
    {
        bool sucessful = false;

        for (int i = 0; i < potions.Length; i++)
        {


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


                foreach (Item item in droppedItems)
                {
                    item.decrementAmount();
                }

                clearSlots();
                hideSlots();
                droppedItems.Clear();
                potionDisplay.sprite = temp.getImage();
                potionDisplay.enabled = true;

                audioSource.clip = sucsessfulSound;
                audioSource.Play();

                potionCollection.PotionList.Add(temp);
                sucessful = true;
                break;
            }
        }

        if (!sucessful)
        {
            //unsucsessfulSound.Play();
        }



    }

    #endregion

    #region Book
    [SerializeField] private GameObject bookUI;
    [SerializeField] private GameObject[] pages;
    private bool bookOpen = false;
    private int page = 0;

    public void openBook()
    {
        bookOpen = true;
        bookUI.SetActive(true);
        pages[page].SetActive(true);
    }

    public void closeBook()
    {
        bookOpen = false;
        bookUI.SetActive(false);
        pages[page].SetActive(false);
    }

    public void nextPage()
    {
        if (page < pages.Length - 1)
        {
            pages[page].SetActive(false);
            page++;
            pages[page].SetActive(true);
        }
    }

    public void prevPage()
    {
        if (page >= 1)
        {
            pages[page].SetActive(false);
            page--;
            pages[page].SetActive(true);
        }
    }
    #endregion

}
