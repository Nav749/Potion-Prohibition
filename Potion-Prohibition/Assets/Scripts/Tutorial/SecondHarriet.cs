using System.Collections;
using TMPro;
using UnityEngine;

public class SecondHarriet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private string[] waitingDialogue;
    [SerializeField] private string[] RightDialogue;
    [SerializeField] private string[] WrongDialogue;
    [SerializeField] private string[] EndDialogue;
    private string[] lines;
    [SerializeField] private Sprite Potion;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TMP_Text bottomText;
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public GameObject TextBubble;
    private int index;
    private bool speakable = false;

    [Header("Babbles")]
    [SerializeField] private AudioClip[] dialogueBabbleClips;
    [SerializeField] private AudioSource audioSourceBabble;
    [SerializeField] private bool stopAudioSource;
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 3f;

    private void Start()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        image.sprite = Potion;
        bottomText.text = "Annihilator 3000";
        lines = waitingDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = (player.transform.position - transform.position).normalized;
        Quaternion lookingRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotation, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (speakable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    //StopAllCoroutines();
                    //textComponent.text = lines[index];
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
            TextBubble.SetActive(false);
            index = 0;
            speakable = false;
        }
    }
}
