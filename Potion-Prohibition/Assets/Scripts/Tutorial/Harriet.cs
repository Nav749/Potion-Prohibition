using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Harriet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private string[] lines;
    [SerializeField] private Sprite Potion;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TMP_Text bottomText;
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public GameObject TextBubble;
    private int index;
    public bool speakable = false;

    [Header("Babbles")]
    [SerializeField] private AudioClip[] dialogueBabbleClips;
    [SerializeField] private AudioSource audioSourceBabble;
    [SerializeField] private bool stopAudioSource;
    [Range (-3,3)]
    [SerializeField] private float minPitch = 0.5f;
    [Range (-3,3)]
    [SerializeField] private float maxPitch = 3f;

    // Update is called once per frame
    void Update()
    {
        Vector3 target = (player.transform.position - transform.position).normalized;
        Quaternion lookingRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotation, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (speakable)
        {
            player.GetComponent<playerMovement>().setMoveLock(true);
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
    }

    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        speakable = true;
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
            if (stopAudioSource) 
            {
                audioSourceBabble.Stop();
            }
            int randomIndex = Random.Range(0, dialogueBabbleClips.Length);
            AudioClip soundClip = dialogueBabbleClips[randomIndex];
            audioSourceBabble.pitch = Random.Range(minPitch, maxPitch);
            audioSourceBabble.PlayOneShot(soundClip);
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
            player.GetComponent<playerMovement>().setMoveLock(false);
            TextBubble.SetActive(false);
            index = 0;
            speakable = false;
            this.transform.GetChild(1).gameObject.SetActive(true);
            image.sprite = Potion;
            bottomText.text = "Annihilator 3000";
        }
    }
}
