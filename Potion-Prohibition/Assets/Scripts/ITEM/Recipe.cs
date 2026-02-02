using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private Item[] items;
    [SerializeField] private Potion Potion;
    public Item[] GetItems()
    {
        return items;
    }

    public Potion GetPotion()
    {
        return Potion;
    }

    public bool checkIngredients(Item[] input)
    {
        return (items == input);
    }
}
