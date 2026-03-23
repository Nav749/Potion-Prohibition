using UnityEngine;

public class TriggerCLone : MonoBehaviour
{
    [SerializeField] SecondHarriet Harriet;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject player;
    [SerializeField] Camera camera;
    private bool playerToggle;

    private void Update()
    {
        Debug.Log(Harriet.canOrder);

        if (Harriet.canOrder)
        {
            if(!canvas.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
            {
                canvas.SetActive(true);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Harriet.StartDialogue();
        }
    }
}
