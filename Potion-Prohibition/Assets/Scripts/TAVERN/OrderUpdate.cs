using TMPro;
using UnityEngine;

public class OrderUpdate : MonoBehaviour
{
    private int orderQuota;
    private int orderDone;
    [SerializeField] private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orderQuota = GameManager.Instance.orderQuota;
        orderDone = GameManager.Instance.currentOrderQuota;
    }

    // Update is called once per frame
    void Update()
    {
        orderQuota = GameManager.Instance.orderQuota;
        orderDone = GameManager.Instance.currentOrderQuota;

        text.text = orderDone.ToString() + "/" + orderQuota.ToString();
    }
}
