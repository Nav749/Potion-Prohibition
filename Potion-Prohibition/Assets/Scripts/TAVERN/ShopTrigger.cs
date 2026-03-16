using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject ShopScreen;
    [SerializeField] Camera cam;

    private bool activeShop = false;
    private bool playerToggle = false;

    private void Update()
    {
        if (activeShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerToggleSwap(playerToggle);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                playerToggleSwap(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            activeShop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            activeShop = false;
        }
    }

    void playerToggleSwap(bool input)
    {
        playerToggle = !input;
        ShopScreen.SetActive(playerToggle);
        Cursor.lockState = playerToggle ? CursorLockMode.None : CursorLockMode.Locked;
        cam.enabled = playerToggle;
        GameManager.Instance.PlayerGO.transform.GetChild(0).gameObject.SetActive(!playerToggle);
        GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(playerToggle);
        GameManager.Instance.PlayerGO.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = !playerToggle;
    }
}
