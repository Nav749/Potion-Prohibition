using UnityEngine;

public class GameManegerTK : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private class Inventory : ScriptableObject
    {
        private Item[] items;

        public Inventory(Item[] items)
        {
            this.items = items;
        }
    }
}

