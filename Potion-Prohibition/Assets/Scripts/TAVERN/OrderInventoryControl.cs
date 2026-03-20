using UnityEngine;

public class OrderInventoryControl : MonoBehaviour
{
    [SerializeField] GameObject[] prefabForItem;
    InventoryItem script;

    // Update is called once per frame
    public void UpdateUI()
    {
        ClearItems();
        SpawnPotions();
    }

    void ClearItems()
    {
        for (int i = 0; i < 15; i++)
        {
            if(this.transform.GetChild(1).GetChild(0).GetChild(i).childCount != 0)
                Destroy(this.transform.GetChild(1).GetChild(0).GetChild(i).GetChild(0).gameObject);
        }

        if (this.transform.GetChild(2).GetChild(0).childCount != 0)
            Destroy(this.transform.GetChild(2).GetChild(0).GetChild(0).gameObject);
    }

    void SpawnPotions()
    {
        for(int i = 0; i < GameManager.Instance.potions.Count; i++)
        {
            if (i == prefabForItem.Length) return;
            script = prefabForItem[i].GetComponent<InventoryItem>();
            script.potion = GameManager.Instance.potions[i];
            Instantiate(prefabForItem[i], this.transform.GetChild(1).GetChild(0).GetChild(i).position, Quaternion.identity, this.transform.GetChild(1).GetChild(0).GetChild(i));
        }
    }
}
