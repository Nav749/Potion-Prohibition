using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] Harriet Harriet;
    private bool talkingTime = false;

    private void Update()
    {
        if(talkingTime && Input.GetKeyDown(KeyCode.E) && !Harriet.speakable)
        {
            Harriet.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkingTime = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            talkingTime = false;
        }
    }
}
