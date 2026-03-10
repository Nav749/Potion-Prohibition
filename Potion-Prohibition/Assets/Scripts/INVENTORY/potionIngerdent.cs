using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class potionIngerdent : MonoBehaviour
{
    private Item item;
    private Image icon;
    [SerializeField] TextMeshProUGUI title;

    private void Awake()
    {
        icon = GetComponent<Image>();
        updateVisuals();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setItem(Item item) {
        this.item = item;
        updateVisuals();
    }

    public void updateVisuals() {
        icon.sprite = item.getIcon();
        title.text = item.getName();
    }

    

}
