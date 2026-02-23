using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SpriteRenderer image;
    private GameManager gameManager;
    [SerializeField] private Item item;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManeger").GetComponent<GameManager>();
        image.sprite = item.getImage();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < gameManager.inventory.Length; i++)
        {
            if (gameManager.inventory[i] == item)
            {
                gameManager.inventory[i].incrmentAmount();
                break;
            }

        }
        GameObject.Destroy(gameObject);
    }

}
