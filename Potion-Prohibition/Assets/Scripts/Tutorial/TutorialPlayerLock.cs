using UnityEngine;

public class TutorialPlayerLock : MonoBehaviour
{
    // other game objects and compontes
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject filter;
    [SerializeField] private Camera craftingCam;
    [SerializeField] private Canvas UI;
    [SerializeField] private Canvas interact;
    [SerializeField] private TutorialCraftingLogic craftingLogic;

    // vars for this class
    public bool isCrafting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCrafting = false;
        filter.SetActive(isCrafting);
        craftingCam = GetComponentInChildren<Camera>();
        craftingCam.enabled = isCrafting;
        UI = GetComponentInChildren<Canvas>();
        UI.enabled = isCrafting;
        interact.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCrafting)
        {
            craftingLogic.clearPot();
            craftingLogic.distroyItems();
            toggleCrafting();
        }
        if (Input.GetKeyDown(KeyCode.E) && canCraft && !isCrafting)
        {
            toggleCrafting();
            craftingLogic.createItems();

        }

    }

    private bool canCraft = false;
    private void OnTriggerEnter(Collider other)
    {

        canCraft = true;

        interact.enabled = true;

    }

    private void OnTriggerExit(Collider other)
    {
        canCraft = false;
        interact.enabled = false;
    }

    private void toggleCrafting()
    {
        isCrafting = !isCrafting;
        filter.SetActive(isCrafting);
        craftingCam.enabled = isCrafting;
        UI.enabled = isCrafting;
        toggleCursor();
        player.GetComponent<playerMovement>().setMoveLock(isCrafting);
        player.GetComponent<playerSpellShoot>().setCrafting(isCrafting);

    }

    public void toggleCursor()
    {
        if (isCrafting)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (!isCrafting)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


}
