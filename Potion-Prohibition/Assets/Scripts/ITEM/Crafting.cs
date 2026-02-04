using System.Linq.Expressions;
using System.Xml;
using UnityEngine;
using UnityEngine.Animations;

public class Crafting : MonoBehaviour
{
    [SerializeField] private Potion[] recipeBook;
    [SerializeField] private GameObject player;
    private Camera craftingCam;
    private bool isCrafting;
    private Canvas UI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCrafting = false;
        craftingCam = GetComponentInChildren<Camera>();
        craftingCam.enabled = isCrafting;
        UI = GetComponentInChildren<Canvas>();
        UI.enabled = isCrafting;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCrafting) {
            toggleCrafting();
        }
        if (isCrafting) { 
            player.transform.position = craftingCam.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        toggleCrafting();
        
    }

    private void toggleCrafting() {
        isCrafting= !isCrafting;
        craftingCam.enabled = isCrafting;
        UI.enabled = isCrafting;
        toggleCursor();
    }

    public void toggleCursor() {
        if (isCrafting)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (!isCrafting) 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
