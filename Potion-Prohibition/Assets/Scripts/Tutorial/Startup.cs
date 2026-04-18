using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] TutorialInventory inventory;
    [SerializeField] playerMovement Player;
    [SerializeField] bool enableJump;
    public bool isLoading;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        isLoading = true;
        LoadingScreen.SetActive(true);
        Player.setMoveLock(true);

        foreach (var item in inventory.inventory)
        {
            item.resetAmount();
        }

        if (enableJump)
        {
            Player.setJumpLock();
        }

        Invoke("TurnOff", 2.5f);
    }

    void TurnOff()
    {
        LoadingScreen.SetActive(false);
        Player.setMoveLock(false);
        isLoading = false;
    }
}
