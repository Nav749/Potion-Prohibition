using UnityEngine;

public class TriggerforcustomerDialogue : MonoBehaviour

{
    [SerializeField] CustomerPool pool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) pool.StartSpeaking();
    }
}
