using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventroyRow : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI amount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.sprite = item.getIcon();
        title.text = item.getName();

    }

    // Update is called once per frame
    void Update()
    {
        amount.text =  "x" + item.getAmount();
    }

  
}
