using NUnit.Framework.Internal;
using System.Linq.Expressions;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerLock : MonoBehaviour
{
    // other game objects and compontes
    [SerializeField] private GameObject player;
    [SerializeField] private Camera craftingCam;
    [SerializeField] private Canvas UI;
    [SerializeField] private CraftingLogic craftingLogic;

    // vars for this class
    private bool isCrafting;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        toggleCrafting();
        //craftingLogic.createItems();
    }
    private void toggleCrafting() {
        isCrafting= !isCrafting;
        craftingCam.enabled = isCrafting;
        UI.enabled = isCrafting;
        toggleCursor();
        player.GetComponent<playerMovement>().setMoveLock(isCrafting);
        player.GetComponent<playerSpellShoot>().setCrafting(isCrafting);

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
