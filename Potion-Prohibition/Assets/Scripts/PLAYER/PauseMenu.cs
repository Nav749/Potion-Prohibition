using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject uiElements;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !GameManager.Instance.inMenu) {
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
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        

    }

    public void resume() 
    { 
        isPaused = false;
        uiElements.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
