using System.Collections;
using TMPro;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    private bool isSpeaking = true;

    public bool canStart = true;

    public TextMeshProUGUI textComponet;
    [SerializeField] string[] linesIntro;
    [SerializeField] string[] linesPasstime;
    [SerializeField] string[] linesRight;
    [SerializeField] string[] linesWrong;
    [SerializeField] float textSpeed;
    [SerializeField] SpriteRenderer potion;
    [SerializeField] TMP_Text text;
    private int textIndex;
    private int dialoguetime;
    private string[] lines;
    private Sprite potionImage;
    private bool commmenedOrder = false;


    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        lines = linesIntro;
        text.text = string.Empty;
    }

    private void Update()
    {
        if (!commmenedOrder) CommenceOrder();

        if(lines == linesPasstime) gameObject.transform.GetChild(1).gameObject.SetActive(true);
        else gameObject.transform.GetChild(1).gameObject.SetActive(false);

        if(Input.GetKeyDown(KeyCode.F) && lines == linesPasstime)
        {
            
        }

        if (isSpeaking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponet.text == lines[textIndex])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponet.text = lines[textIndex];
                }
            }
        }
    }

    public void StartDialogue()
    {
        textComponet.text = string.Empty;
        textIndex = 0;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        canStart = false;
        foreach (char c in lines[textIndex].ToCharArray())
        {
            textComponet.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        canStart = true;
    }

    void NextLine()
    {
        if (textIndex < lines.Length - 1)
        {
            textIndex++;
            textComponet.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            canStart = true;
            lines = linesPasstime;
        }
    }

    void CommenceOrder()
    {
        potionImage = GameManager.Instance.currentOrder.getImage();
        potion.sprite = potionImage;
        potion.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        text.text = GameManager.Instance.currentOrder.BottomText();
        commmenedOrder = true;
    }

}
