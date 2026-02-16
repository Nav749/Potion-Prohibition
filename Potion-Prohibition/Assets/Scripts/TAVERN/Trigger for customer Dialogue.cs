using UnityEngine;

public class TriggerforcustomerDialogue : MonoBehaviour

{
    [SerializeField] CustomerPool pool;
    public bool canSpeak = false;

    private void Update()
    {
        if (canSpeak && Input.GetKeyDown(KeyCode.E)) pool.StartSpeaking();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSpeak = true;
            pool.OrderToggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSpeak = false;
            pool.OrderToggle();
        }
    }
}
