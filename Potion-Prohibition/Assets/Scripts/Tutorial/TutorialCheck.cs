using UnityEngine;

public class TutorialCheck : MonoBehaviour
{
    [SerializeField] GameObject planeblock;
    [SerializeField] TutorialInventory inventory;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ItemLoop() == 2)
        {
            planeblock.SetActive(false);
        }
    }

    private int ItemLoop()
    {
        int num = 0;
        
        foreach (var item in inventory.inventory)
        {
            if (item.getAmount() == 1) num++;
        }

        return num;
    }
}
