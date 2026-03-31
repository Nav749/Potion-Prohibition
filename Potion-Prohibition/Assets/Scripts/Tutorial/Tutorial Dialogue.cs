using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] GameObject portalBox;
    [SerializeField] GameObject portalParticles;
    [SerializeField] GameObject blocker;
    [SerializeField] playerMovement player;
    [SerializeField] Fade fade;

    [Header("Babbles")]
    [SerializeField] private AudioClip[] dialogueBabbleClips;
    [SerializeField] private AudioSource audioSourceBabble;
    [SerializeField] private bool stopAudioSource;
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 3f;
    [SerializeField] private int frequency = 2;
    private int counter;

    [SerializeField] List<TutorialSO> tutorials;
    [SerializeField] List<Vector3> positions;
    [SerializeField] List<Quaternion> rotation;
    private string[] lines;

    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public GameObject TextBubble;
    private int index;
    private int currentIndex = 0;
    private bool speakable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = positions[currentIndex];
        this.transform.rotation = rotation[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
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
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }


    }

    void StartDialogue()
    {
        textComponent.text = string.Empty;
        Invoke("lockPlayer", 0.3f);
        index = 0;
        StartCoroutine(TypeLine());
        fade.FadeIn();
    }

    void lockPlayer()
    {
        player.setMoveLock(speakable);
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            counter++;
            if (stopAudioSource)
            {
                audioSourceBabble.Stop();
            }
            if (counter == frequency)
            {
                int randomIndex = Random.Range(0, dialogueBabbleClips.Length);
                AudioClip soundClip = dialogueBabbleClips[randomIndex];
                audioSourceBabble.pitch = Random.Range(minPitch, maxPitch);
                audioSourceBabble.PlayOneShot(soundClip);
                counter = 0;
            }
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
            fade.FadeOut();
            index = 0;
            speakable = false;
            lockPlayer();
            Invoke("Wait", 1f);
        }
    }

    private void Wait()
    {
        fade.ResetAnim();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speakable = true;
            lines = tutorials[currentIndex].GetDialogue();
            TextBubble.SetActive(true);
            StartDialogue();
            currentIndex++;
            if (currentIndex == tutorials.Count)
            {
                portalBox.SetActive(false);
                portalParticles.SetActive(true);
                blocker.SetActive(true);
                currentIndex = 0;
            }
            this.transform.position = positions[currentIndex];
            this.transform.rotation = rotation[currentIndex];
        }
    }

}
