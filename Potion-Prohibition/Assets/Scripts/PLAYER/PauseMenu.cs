using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject uiElements;
    [SerializeField] private GameObject contols;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.inMenu && !GameManager.Instance.isLoading) {
            if (isPaused)
            {
                resume();
            }
            else if (!isPaused)
            {
                pause();
            }
        }
    }

    private void pause() 
    { 
        isPaused = true;
        uiElements.SetActive(true);
        GameManager.Instance.inMenu = true;
        GameManager.Instance.lockCamara(isPaused);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        

    }

    public void resume() 
    { 
        isPaused = false;
        uiElements.SetActive(false);
        GameManager.Instance.inMenu = false;
        GameManager.Instance.lockCamara(isPaused);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void showContols() { 
        contols.SetActive(true);
    }

    public void hideContols() { 
        contols.SetActive(false);

    }
}
