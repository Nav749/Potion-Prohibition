using System.Collections;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool isSpeaking = true;

    public TextMeshProUGUI textComponet;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed;
    private int textIndex;

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
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[textIndex].ToCharArray())
        {
            textComponet.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
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
            gameObject.SetActive(false);
        }
    }

}
