using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] GameObject portalBox;
    [SerializeField] GameObject portalParticles;
    [SerializeField] GameObject blocker;
    [SerializeField] playerMovement player;

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
                    //StopAllCoroutines();
                    //textComponent.text = lines[index];
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
            speakable = false;
            lockPlayer();
        }
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
