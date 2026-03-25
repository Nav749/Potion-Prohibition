using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditLine : MonoBehaviour
{
    [SerializeField] private Sprite PFP;
    [SerializeField] private Image PFPImage;
    [SerializeField] private string Credit;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PFPImage.sprite = PFP;
        textMeshPro.text = Credit;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
