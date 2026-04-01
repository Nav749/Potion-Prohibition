using TMPro;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject ShopScreen;
    [SerializeField] GameObject interactTalk;
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI damage;

    private bool activeShop = false;
    private bool playerToggle = false;


    private void Update()
    {
        health.text = GameManager.Instance.healthPrice.ToString() + " coins";
        damage.text = GameManager.Instance.damagePrice.ToString() + " coins";


        if (activeShop)
        {
            if (Input.GetKeyDown(KeyCode.E) && !GameManager.Instance.inMenu)
            {

                playerToggleSwap(playerToggle);

            }
            else if ((Input.GetKeyDown(KeyCode.Escape) && playerToggle) || (Input.GetKeyDown(KeyCode.E) && playerToggle))
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
            interactTalk.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            activeShop = false;
            interactTalk.SetActive(false);
        }
    }

    void playerToggleSwap(bool input)
    {
        playerToggle = !input;
        interactTalk.SetActive(!playerToggle);
        GameManager.Instance.alwayson.SetActive(!playerToggle);
        ShopScreen.SetActive(playerToggle);
        Cursor.lockState = playerToggle ? CursorLockMode.None : CursorLockMode.Locked;
        cam.enabled = playerToggle;
        GameManager.Instance.PlayerGO.transform.GetChild(0).gameObject.SetActive(!playerToggle);
        GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(playerToggle);
        GameManager.Instance.PlayerGO.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = !playerToggle;
        if (playerToggle)
        {
            GameManager.Instance.inMenu = true;
        }
        else if (!playerToggle)
        {
            Invoke("changeInMenu", 0.1f);
        }


    }

    private void changeInMenu() { GameManager.Instance.inMenu = false; }

    public void BuyHealth()
    {
        if (GameManager.Instance.coins >= GameManager.Instance.healthPrice)
        {
            GameManager.Instance.UpdateHealth();
            GameManager.Instance.coins -= GameManager.Instance.healthPrice;
            GameManager.Instance.healthPrice = (int)(GameManager.Instance.healthPrice * 1.1f);
        }
    }

    public void BuyDamage()
    {
        if (GameManager.Instance.coins >= GameManager.Instance.damagePrice)
        {
            GameManager.Instance.UpdateDamage();
            GameManager.Instance.coins -= GameManager.Instance.damagePrice;
            GameManager.Instance.damagePrice = (int)(GameManager.Instance.damagePrice * 1.1f);
        }
    }
}
