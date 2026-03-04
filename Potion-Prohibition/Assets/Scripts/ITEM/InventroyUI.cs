using UnityEngine;
using UnityEngine.UIElements;

public class InventroyUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private int page;
    [SerializeField] private GameObject[] pages;

    private bool open;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        page = 0;
        open = false;
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
