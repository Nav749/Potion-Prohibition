using UnityEngine;

public class TriggerforcustomerDialogue : MonoBehaviour

{
    [SerializeField] CustomerPool pool;
    [SerializeField] GameObject UI;
    [SerializeField] Camera camera;
    public bool canSpeak = false;
    private bool playerToggle = false;

    private void Update()
    {
        if (canSpeak && Input.GetKeyDown(KeyCode.E)) pool.StartSpeaking();

        if (GameManager.Instance.orderTime)
        {
            if (canSpeak && Input.GetKeyDown(KeyCode.F))
            {
                togglePlayer();
            }
        }
        else
        {
            togglePlayerFalse();
        }

    }

    private void togglePlayer()
    {
        playerToggle = !playerToggle;
        UI.SetActive(playerToggle);
        Cursor.lockState = playerToggle ? CursorLockMode.None : CursorLockMode.Locked; //sets the lock state as none if true, locked if false
        camera.enabled = playerToggle;
        GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(playerToggle);
        GameManager.Instance.PlayerGO.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = !playerToggle;
    }

    private void togglePlayerFalse()
    {
        playerToggle = false;
        UI.SetActive(playerToggle);
        Cursor.lockState = playerToggle ? CursorLockMode.None : CursorLockMode.Locked; //sets the lock state as none if true, locked if false
        camera.enabled = playerToggle;
        GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(playerToggle);
        GameManager.Instance.PlayerGO.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = !playerToggle;
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

    public void OrderCheck()
    {
        GameManager.Instance.TimeToCheckOrder();
        Debug.Log(GameManager.Instance.correctOrder);
        togglePlayer();
    }
}
