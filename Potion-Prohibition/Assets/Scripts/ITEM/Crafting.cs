using NUnit.Framework.Internal;
using System.Linq.Expressions;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class Crafting : MonoBehaviour
{
    // other game objects and compontes
    [SerializeField] private Potion[] recipeBook;
    [SerializeField] private GameObject player;
    [SerializeField] private  GameObject ingredientPrefab;
    private Camera craftingCam;
    private Canvas UI;

    // vars for this class
    private bool isCrafting;
    private Item[] input;
    private bool onTheRocks = false;
    private bool spiced = false;
    private int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCrafting = false;
        craftingCam = GetComponentInChildren<Camera>();
        craftingCam.enabled = isCrafting;
        UI = GetComponentInChildren<Canvas>();
        UI.enabled = isCrafting;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCrafting) {
            toggleCrafting();
        }
        if (isCrafting) { 
            player.transform.position = craftingCam.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        toggleCrafting();
        
    }
    private void toggleCrafting() {
        isCrafting= !isCrafting;
        craftingCam.enabled = isCrafting;
        UI.enabled = isCrafting;
        toggleCursor();
        player.GetComponent<playerMovement>().setMoveLock(isCrafting);
        player.GetComponent<playerSpellShoot>().setCrafting(isCrafting);

    }

    public void toggleCursor() {
        if (isCrafting)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (!isCrafting) 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void addIngredient(Item item) {
        if (item.getName() == "Spices")
        {
            spiced = true;
        }
        else if (item.getName() == "Rocks")
        {
            onTheRocks = true;
        }
        else 
        {
            input[count] = item;
            count++;
        }


    }


}
