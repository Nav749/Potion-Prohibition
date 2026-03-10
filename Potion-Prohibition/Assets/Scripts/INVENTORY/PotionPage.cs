using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionPage : MonoBehaviour
{
    [SerializeField] private Potion potion;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject[] ingredents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        List<Item> items = potion.getIngredients();
        
        ingredents[0].GetComponent<potionIngerdent>().setItem(items[0]);
        ingredents[1].GetComponent<potionIngerdent>().setItem(items[1]);
        if (items.Count == 2) {
            ingredents[2].SetActive(false);
        }

        if (items.Count == 3)
        {
            ingredents[2].GetComponent<potionIngerdent>().setItem(items[2]);
        }
   
    }

    void Awake()
    {
        updateVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateVisuals() {
        icon.sprite = potion.getIcon();
        title.text = potion.getName();
        

    }
}
