using UnityEngine;

public class TutorialPickup : MonoBehaviour
{
    private SpriteRenderer image;
    [SerializeField] private Item item;
    [SerializeField] GameObject player;
    [SerializeField] TutorialInventory inventory;
    private AudioSource pickupSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupSource = GetComponent<AudioSource>();
        image = GetComponent<SpriteRenderer>();
        image.sprite = item.getImage();
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(inventory == null)
        {
            inventory = GameObject.Find("Inventory").GetComponent<TutorialInventory>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickupSource.Play();
            Debug.Log(item.getName() + " pciked up");
            for (int i = 0; i < inventory.inventory.Length; i++)
            {
                if (inventory.inventory[i] == item)
                {
                    inventory.inventory[i].incrmentAmount();
                    break;
                }

            }


            pickupSource?.Play();
            image.enabled = false;
            GetComponent<BoxCollider>().enabled = false;



            Invoke("distroy", 2);
        }
    }


    private void distroy()
    {
        GameObject.Destroy(gameObject);

    }
}
