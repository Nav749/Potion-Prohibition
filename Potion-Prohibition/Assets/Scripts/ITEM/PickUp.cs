using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SpriteRenderer image;
    private GameManegerTK gameManeger;
    [SerializeField] private Item item;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        gameManeger = GameObject.Find("GameManeger").GetComponent<GameManegerTK>();
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
        // gameManeger.updateInventroy(item.getID());

    }

}
