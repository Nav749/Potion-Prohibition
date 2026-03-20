using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] Harriet Harriet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Harriet.StartDialogue();
        }
    }
}
