using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonSceneLoad : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.LoadLevels();
        GameManager.Instance.DeleteGameManager();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        GameManager.Instance.DeleteGameManager();
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Comic");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RetryTutorial()
    {
        SceneManager.LoadScene("Tutorial2");
    }

    public void MainMenuTutorial()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
