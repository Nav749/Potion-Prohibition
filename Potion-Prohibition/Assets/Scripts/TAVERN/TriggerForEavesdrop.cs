using System.Collections;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TriggerForEavesdrop : MonoBehaviour
{
    private bool playerListening;

    [SerializeField] CustomerDialogue dialogue;
    private string[] lines;

    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public GameObject TextBubble;

    private int index;

    private void Start()
    {
        lines = dialogue.currentCustomers.GetDialogue();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (playerListening)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            TextBubble.SetActive(false);
            index = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerListening = true;
            TextBubble.SetActive(true);
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerListening = false;
            TextBubble.SetActive(false);
            StopAllCoroutines();
        }
    }
}
