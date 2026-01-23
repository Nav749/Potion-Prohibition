using UnityEngine;

public class GameManegerTK : MonoBehaviour
{

    [SerializeField] private Item[] inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInventroy(int item) 
    {
        inventory[item].incrmentAmount();
    }
}

