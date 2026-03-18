using UnityEngine;

public class TriggerforcustomerDialogue : MonoBehaviour

{
    [SerializeField] CustomerPool pool;
    [SerializeField] GameObject UI;
    [SerializeField] Camera camera;
    [SerializeField] OrderInventoryControl orderInventoryControl;
    public bool canSpeak = false;
    private bool playerToggle = false;
    private bool isOrdering = false;

    private void Update()
    {
        if (canSpeak && Input.GetKeyDown(KeyCode.E) && !isOrdering && !pool.currentCustomer.GetComponent<Customer>().isSpeaking) pool.StartSpeaking();

        if (GameManager.Instance.orderTime)
        {
            if (canSpeak && Input.GetKeyDown(KeyCode.F))
            {
                if (!playerToggle)
                {
                    orderInventoryControl.UpdateUI();
                }
                togglePlayer();
            }
        }

    }

    private void togglePlayer()
    {
        playerToggle = !playerToggle;
        isOrdering = !isOrdering;
        UI.SetActive(playerToggle);
        Cursor.lockState = playerToggle ? CursorLockMode.None : CursorLockMode.Locked; //sets the lock state as none if true, locked if false
        
        GameManager.Instance.PlayerGO.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = !playerToggle;
        camera.enabled = playerToggle;
        GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(playerToggle);
        GameManager.Instance.PlayerGO.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = !playerToggle;
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
        if(GameManager.Instance.OrdertoCheck != null)
        {
            GameManager.Instance.TimeToCheckOrder();
            Debug.Log(GameManager.Instance.correctOrder);
            togglePlayer();
        }
    }
}
