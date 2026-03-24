using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialNextDay : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("Persistent");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
