using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] playerMovement Player;
    [SerializeField] bool enableJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadingScreen.SetActive(true);
        Player.setMoveLock(true);

        if (enableJump)
        {
            Player.setJumpLock();
        }

        Invoke("TurnOff", 2f);
    }

    void TurnOff()
    {
        LoadingScreen.SetActive(false);
        Player.setMoveLock(false);
    }
}
