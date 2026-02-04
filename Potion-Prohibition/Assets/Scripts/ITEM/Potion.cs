using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    [SerializeField] private Item[] ingredients;



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

}
