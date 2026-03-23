using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameFeild;
    [SerializeField] private TextMeshProUGUI descriptionFeild;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        icon.sprite = item.getIcon();
        nameFeild.text = item.getName();
        descriptionFeild.text = item.getDescription();
    }



}
