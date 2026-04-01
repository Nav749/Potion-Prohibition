using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ComicMove : MonoBehaviour
{
    public float limit;
    public GameObject Buttons;
    public VideoPlayer video;
    public GameObject SkipButton;

    // Update is called once per frame
    void Update()
    {
        if (!video.isPlaying)
        {
            Buttons.SetActive(true);
            SkipButton.SetActive(false);
        }
        else
        {
            Buttons.SetActive(false);
            SkipButton.SetActive(true);
        }
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Persistent");
    }

    public void Skip()
    {
        video.time = video.length;
    }
}
