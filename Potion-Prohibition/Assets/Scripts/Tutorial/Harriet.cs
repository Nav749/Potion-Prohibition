using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Harriet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private string[] lines;

    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public GameObject TextBubble;
    private int index;

    // Update is called once per frame
    void Update()
    {
        Vector3 target = (player.transform.position - transform.position).normalized;
        Quaternion lookingRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotation, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (Input.GetKeyDown(KeyCode.E))
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

    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        TextBubble.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            textComponent.ForceMeshUpdate();
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
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
}
