using System.ComponentModel;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private int ID;
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite icon;
    [SerializeField] private int amount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getID()
    {
        return ID;
    }

    
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
    public Sprite getImage() {
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
    public void incrmentAmount() 
    {
        amount++;
    }

    public void decrementAmount() 
    { 
        amount--;
    }

     public int getAmount() 
    {
             return amount;
    }

}
