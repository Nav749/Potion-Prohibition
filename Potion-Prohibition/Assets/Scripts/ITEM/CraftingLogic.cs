using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject dragablePrefab;
    [SerializeField] private RectTransform rectTransform;
    
    public void createItems() {
        for (int i = 0; i <= gameManager.inventory.Length; i++)
        {
            dragablePrefab.GetComponent<ItemDrag>().setItem(gameManager.inventory[i]);
            //Debug.Log(gameManager.inventory[i].getAmount() + gameManager.inventory[i].getName());
            for (int j = 0; j < gameManager.inventory[i].getAmount(); j++) 
            {
               
                Instantiate(dragablePrefab, rectTransform);
            }
        }
    }


    public void testCreate() { 
        dragablePrefab.GetComponent<ItemDrag>().setItem(gameManager.inventory[3]);
        Instantiate(dragablePrefab, rectTransform);
    }
    #endregion



}
