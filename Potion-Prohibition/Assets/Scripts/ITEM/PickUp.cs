using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SpriteRenderer image;
    private GameManager gameManager;
    [SerializeField] private Item item;
    private GameObject player;

    [SerializeField] private AudioClip pickupSFX;
    private AudioSource pickupSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupSource = GetComponent<AudioSource>();
        pickupSource.clip = pickupSFX;
        image = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
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
        Debug.Log(item.getName() + " pciked up");
        for (int i = 0; i < gameManager.inventory.Length; i++)
        {
            if (gameManager.inventory[i] == item)
            {
                pickupSource.Play();
                gameManager.inventory[i].incrmentAmount();
                break;
            }

        }
        GameObject.Destroy(gameObject);
    }

}
