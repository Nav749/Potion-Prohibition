using UnityEngine;

public class TriggerforcustomerDialogue : MonoBehaviour

{
    [SerializeField] CustomerPool pool;
    [SerializeField] GameObject interactTalk;
    [SerializeField] GameObject orderTalk;
    [SerializeField] GameObject UI;
    [SerializeField] Camera camera;
    [SerializeField] OrderInventoryControl orderInventoryControl;
    public bool canSpeak = false;
    private bool playerToggle = false;
    private bool isOrdering = false;

    private void Update()
    {
        if (canSpeak 
            && Input.GetKeyDown(KeyCode.E) 
            && !isOrdering 
            && !pool.currentCustomer.GetComponent<Customer>().isSpeaking 
            && GameManager.Instance.speakable 
            && !GameManager.Instance.inMenu
            )
        {
            pool.StartSpeaking();
            GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(true);
        }

        if (GameManager.Instance.orderTime)
        {
            orderTalk.SetActive(!isOrdering);
            interactTalk.SetActive(!isOrdering);
            if (canSpeak && (Input.GetKeyDown(KeyCode.F) || (Input.GetKeyDown(KeyCode.Escape) && playerToggle) ))
            {
                orderTalk.SetActive(false);
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
        GameManager.Instance.inMenu = playerToggle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSpeak = true;
            interactTalk.SetActive(true);
            pool.OrderToggle();
            GameManager.Instance.speakable = true;
            if (GameManager.Instance.orderTime) { isOrdering = false; }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSpeak = false;
            interactTalk.SetActive(false);
            pool.OrderToggle();
            GameManager.Instance.speakable = false;
            if (GameManager.Instance.orderTime) { isOrdering= true; }
        }
    }

    public void OrderCheck()
    {
        if(GameManager.Instance.OrdertoCheck != null)
        {
            GameManager.Instance.TimeToCheckOrder();
            togglePlayer();
        }
    }
}
