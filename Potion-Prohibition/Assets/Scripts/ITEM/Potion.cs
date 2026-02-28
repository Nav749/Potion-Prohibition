using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    [SerializeField] private List<Item> ingredients;
    [SerializeField] private string bottomText;
    private bool onTheRocks = false;
    private bool spiced = false;

    public string BottomText()
    {
        return bottomText;
    }

    public string getName()
    {
        return name;
    }

    public Sprite getImage()
    {
        return image;
    }

    public bool checkIngredients(Item[] input) {
        bool check = true;
        foreach (Item item in input) {
            if (!ingredients.Contains(item)) {
                check = false;
            }
        }
        //make sure that null slots dont fuck things up
        return check;
    }

    public void setSpiced(bool input)
    {
        spiced = input;

    }

    public void setOnTheRocks(bool input)
    {
        onTheRocks = input;
    }

    public bool isSpiced()
    {
        return spiced;
    }

    public bool isOnTheRocks() 
    {
        return onTheRocks;
    }

    public string itemsToString() {
        string temp = "";
        for (int i = 0; i < ingredients.Count; i++)
        {
            temp = string.Concat(temp, " ", ingredients[i].getName());
        }

        return temp;
    }
}
