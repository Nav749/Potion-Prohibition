using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite icon;
    [SerializeField] private int amount;

    #region getters and setters



    // Name comands
    public string getName()
    {
        if (name == null)
        {
            Debug.Log("Name value not set");
        }
        return name;
    }

    // discription comands
    public string getDescription()
    {
        if (description == null)
        {
            Debug.Log("Discription value not set");
        }
        return description;
    }

    // image comands
    public Sprite getImage()
    {
        if (image == null)
        {
            Debug.Log("Image not set");
        }
        return image;
    }

    //icon comands
    public Sprite getIcon()
    {
        if (icon == null)
        {
            Debug.Log("Icon not set");
        }
        return icon;
    }

    // amount comands
    public int getAmount()
    {
        return amount;
    }

    public void incrmentAmount()
    {
        amount++;
    }

    public void decrementAmount()
    {
        amount--;
    }

    public void resetAmount()
    {
        amount = 0;
    }
    #endregion

}
