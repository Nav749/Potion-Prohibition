using UnityEngine;

public class InventroyUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject genralUI;

    private int page;
    [SerializeField] private GameObject[] pages;
    private bool open;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        page = 0;
        open = false;
        genralUI.SetActive(open);
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
        //pages[page].SetActive(open);
        genralUI.SetActive(open);
        toggleCursor();
        
    }

    public void toggleCursor()
    {
        if (open)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (!open)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void nextPage()
    {
        if (page < pages.Length - 1)
        {
            pages[page].SetActive(false);
            page++;
            pages[page].SetActive(true);
        }
    }

    public void prevPage() {
        if (page > 1) {
            pages[page].SetActive(false);
            page--;
            pages[page].SetActive(true);

        }
    }

}
