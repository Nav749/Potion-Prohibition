using UnityEngine;

public class TutorialInventory : MonoBehaviour
{
    public Item[] inventory;
    public void clearInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].resetAmount();
        }
    }
}
