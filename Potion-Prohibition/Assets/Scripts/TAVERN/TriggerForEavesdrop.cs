using UnityEngine;

public class TriggerForEavesdrop : MonoBehaviour
{
    private bool playerListening;

    [SerializeField] CustomerDialogue dialogue;
    private string[] lines;

    private void Start()
    {
        lines = dialogue.currentCustomers.GetDialogue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < lines.Length; i++)
            {
                Debug.Log(lines[i]);
            }
        }
    }
}
