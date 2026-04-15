using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] Harriet Harriet;
    [SerializeField] GameObject eToInteract;
    private bool talkingTime = false;

    private void Update()
    {
        if(talkingTime && Input.GetKeyDown(KeyCode.E) && !Harriet.speakable)
        {
            eToInteract.SetActive(false);
            Harriet.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkingTime = true;
            eToInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            talkingTime = false;
            eToInteract.SetActive(false);
        }
    }
}
