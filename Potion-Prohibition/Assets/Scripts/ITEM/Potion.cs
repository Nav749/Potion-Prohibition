using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    [SerializeField] private Item[] ingredients;
    private bool onTheRocks = false;
    private bool spiced = false;



    public string getName()
    {
        return name;
    }

    public Sprite getImage()
    {
        return image;
    }

    public bool checkIngredients(Item[] input) {
        return input == ingredients;
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
}
