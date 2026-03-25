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
    [SerializeField] private int sectionOnePos;
    [SerializeField] private int sectionTwoPos;
    [SerializeField] private int sectionThreePos;


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
        if (!GameManager.Instance.isLoading)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !open && !GameManager.Instance.inMenu)
            {
                toggleInventrory();
            }
            else if ((Input.GetKeyDown(KeyCode.Tab) && open) || (Input.GetKeyDown(KeyCode.Escape) && open) && !GameManager.Instance.inMenu)
            {
                toggleInventrory();
            }
        }
    }
    private void toggleInventrory()
    {
        open = !open;
        pages[page].SetActive(open);
        toggleBackground(open);
        genralUI.SetActive(open);
        toggleCursor();
        if (open)
        {
            GameManager.Instance.inMenu = true;
        }
        else if (!open)
        {
            Invoke("changeInMenu", 1);
        }

    }

    private void changeInMenu() { GameManager.Instance.inMenu = false; }
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

            pages[page].SetActive(false);
            page++;
            switchBackground(page);
            pages[page].SetActive(true);
        }
    }

    public void prevPage()
    {
        if (page > 0)
        {
            pages[page].SetActive(false);
            page--;
            switchBackground(page);
            pages[page].SetActive(true);

        }
    }

    public void goToPage(int index)
    {
        if (index > -1 && index < pages.Length)
        {
            pages[page].SetActive(false);
            page = index;
            switchBackground(page);
            pages[page].SetActive(true);
        }
    }

    public void goToSectionOne() { goToPage(sectionOnePos); }
    public void goToSectionTwo() { goToPage(sectionTwoPos); }
    public void goToSectionThree() { goToPage(sectionThreePos); }

    //toggels backgound to last open page when tab pressed (or just turns them off
    private void toggleBackground(bool input)
    {
        if (input)
        {
            backGrounds[background].SetActive(true);

        }
        else
        {
            backGrounds[background].SetActive(false);
        }
    }

    private void switchBackground(int index)
    {
        if (index == sectionOnePos)
        {
            backGrounds[background].SetActive(false);
            background = 0;
            backGrounds[background].SetActive(true);

        }
        else if (index > sectionOnePos && index < sectionTwoPos)
        {
            backGrounds[background].SetActive(false);
            background = 1;
            backGrounds[background].SetActive(true);


        }
        else if (index == sectionTwoPos)
        {
            backGrounds[background].SetActive(false);
            background = 2;
            backGrounds[background].SetActive(true);
        }
        else if (index > sectionTwoPos && index < sectionThreePos)
        {
            backGrounds[background].SetActive(false);
            background = 3;
            backGrounds[background].SetActive(true);
        }
        else if (index == sectionThreePos)
        {
            backGrounds[background].SetActive(false);
            background = 4;
            backGrounds[background].SetActive(true);
        }
        else if (index > sectionThreePos)
        {
            backGrounds[background].SetActive(false);
            background = 5;
            backGrounds[background].SetActive(true);
        }



    }

}
