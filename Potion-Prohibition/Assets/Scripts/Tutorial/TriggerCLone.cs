using UnityEngine;

public class TriggerCLone : MonoBehaviour
{
    [SerializeField] SecondHarriet Harriet;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject player;
    [SerializeField] Camera camera;
    [SerializeField] TutorialInevtorySlot checker;
    private bool playerToggle;

    private void Update()
    {

        if (Harriet.canOrder)
        {
            if(!canvas.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
            {
                canvas.SetActive(true);
                canvas.GetComponent<TutorialOrderInventoyrtControl>().UpdateUI();
                togglePlayer();
            }
            else if(canvas.activeInHierarchy &&(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape)))
            {
                canvas.SetActive(false);
                togglePlayer();
            }
        }
    }

    private void togglePlayer()
    {
        playerToggle = !playerToggle;
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Harriet.StartDialogue();
        }
    }
}
