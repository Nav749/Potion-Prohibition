using System.Collections;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool isSpeaking = true;

    public bool canStart = true;

    public TextMeshProUGUI textComponet;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed;
    private int textIndex;

    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
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
        }
    }

}
