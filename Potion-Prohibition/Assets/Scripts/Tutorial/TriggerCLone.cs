using UnityEngine;

public class TriggerCLone : MonoBehaviour
{
    [SerializeField] SecondHarriet Harriet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Harriet.StartDialogue();
        }
    }
}
