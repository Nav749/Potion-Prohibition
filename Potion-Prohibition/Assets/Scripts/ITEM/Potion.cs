using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite icon;
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

    public Sprite getIcon() { 
        return icon;
    }

    public List<Item> getIngredients() { 
        return ingredients;
    }

    public bool checkIngredients(List<Item> input) {

        bool check = input[0] == ingredients[0];
        check = check && (input[1] == ingredients[1]);
        if (ingredients.Count == 3)
        {
            check = check && (input[2] == ingredients[2]);
        }
        



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
