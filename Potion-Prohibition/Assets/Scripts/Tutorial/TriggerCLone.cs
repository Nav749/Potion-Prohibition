using UnityEngine;

public class TriggerCLone : MonoBehaviour
{
    [SerializeField] SecondHarriet Harriet;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject player;
    [SerializeField] Camera camera;
    [SerializeField] TutorialInevtorySlot checker;
    private bool playerToggle;
    private bool talkingTime = false;
    [SerializeField] GameObject eToInteract;
    [SerializeField] GameObject fToInteract;

    private void Update()
    {

        if (talkingTime && Input.GetKeyDown(KeyCode.E) && !Harriet.speakable)
        {
            Harriet.StartDialogue();
            eToInteract.SetActive(false);
        }

        if (Harriet.canOrder)
        {
            if(!canvas.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
            {
                canvas.SetActive(true);
                canvas.GetComponent<TutorialOrderInventoyrtControl>().UpdateUI();
                togglePlayer();
                eToInteract.SetActive(false);
                fToInteract.SetActive(false);
            }
            else if(canvas.activeInHierarchy &&(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape)))
            {
                canvas.SetActive(false);
                togglePlayer();
                fToInteract.SetActive(true);
            }
        }
    }

    private void togglePlayer()
    {
        playerToggle = !playerToggle;
        talkingTime = !playerToggle;
        Cursor.lockState = playerToggle ? CursorLockMode.None : CursorLockMode.Locked; //sets the lock state as none if true, locked if false
        player.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = !playerToggle;
        camera.enabled = playerToggle;
        player.GetComponent<playerMovement>().setMoveLock(playerToggle);
        player.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = !playerToggle;
    }

    public void InitiateCheck()
    {
        if(checker.potionToCheck != null)
        {
            Debug.Log(checker.isCorrect);
            canvas.SetActive(false);
            togglePlayer();
            Harriet.changeMind(checker.isCorrect);
            Harriet.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkingTime = true;
            eToInteract.SetActive(true);
            fToInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            talkingTime = false;
            eToInteract.SetActive(false);
            fToInteract.SetActive(false);
        }
    }
}
