using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private Potion Potion;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Recipe(Item[] items)
    {
        this.items = items;
    }

    private Recipe(Item[] items, Potion potion) 
    {
        this.items = items;
        this.Potion = potion;
    }

    public Recipe createRecipe(Item[] items) { 
        return new Recipe(items);
    }

    public Item[] GetItems() 
    {
        return items; 
    }

    public Potion GetPotion()
    {
        return Potion;
    }
}
