using Unity.VisualScripting;
using UnityEngine;

public class InventroyUI : MonoBehaviour
{
    //[SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject genralUI;
    

    private int page;
    [SerializeField] private GameObject[] pages;
    private bool open;

    private int background;
    [SerializeField] private GameObject[] backGrounds;
    [SerializeField] private int backgroundOnePos;
    //[SerializeField] private int backgroundTwoPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        page = 0;
        open = false;
        genralUI.SetActive(open);
        background = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { 
            toggleInventrory();
        }
    }
    private void toggleInventrory()
    {
        open = !open;
        pages[page].SetActive(open);
        toggleBackground(open);
        genralUI.SetActive(open);
        toggleCursor();
        
    }

    public void toggleCursor()
    {
        if (open)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        if (!open)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void nextPage()
    {
        if (page < pages.Length - 1)
        {
            if (page + 1 == backgroundOnePos) 
            {
                switchBackground(background++);
            }
            pages[page].SetActive(false);
            page++;
            pages[page].SetActive(true);
        }
    }

    public void prevPage() {
        if (page > 0) {
            if(page -1 == backgroundOnePos) 
            { 
                switchBackground(background--); 
            }
            pages[page].SetActive(false);
            page--;
            pages[page].SetActive(true);

        }
    }

    public void goToPage(int index) { 
        
    }

    private void toggleBackground(bool input) {
        if (input)
        {
            switchBackground(background);
            
        }
        else {
            backGrounds[background].SetActive(false);
        }
    }

    private void switchBackground(int index) {
        for (int i = 0; i < backGrounds.Length; i++) {
            if (i == index)
            {
                backGrounds[i].SetActive(true);
            }
            else 
            { 
                backGrounds[i].SetActive(false);    
            }
        }
    }

}
