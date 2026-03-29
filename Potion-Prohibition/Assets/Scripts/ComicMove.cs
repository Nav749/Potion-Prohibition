using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicMove : MonoBehaviour
{
    public float limit;
    public GameObject Buttons;
    public GameObject SkipButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < limit)
        this.transform.position += new Vector3(0, 0.005f, 0);
        else if(this.transform.position.y > limit)
        {
            Buttons.SetActive(true);
            SkipButton.SetActive(false);
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
        this.transform.position = new Vector3(0, limit + 0.01f, 0);
    }
}
