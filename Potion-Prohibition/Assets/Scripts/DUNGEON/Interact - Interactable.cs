using UnityEngine;
using UnityEngine.WSA;

public class InteractInteractable : MonoBehaviour
{
    [SerializeField] GameObject spawnableObject;
    private bool activateSpawn = false;

    public int itemsLeft = 3;


    private void Update()
    {
        if (activateSpawn && Input.GetKeyDown(KeyCode.E))
        {
            SpawnItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            activateSpawn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            activateSpawn = false;
        }
    }

    void SpawnItem()
    {
        if (itemsLeft > 0)
        {
            Instantiate(spawnableObject, this.transform.position, Quaternion.identity, this.transform.parent.parent.parent);
            itemsLeft--;
        }
        else
        {
            Debug.Log("No Items Left");
        }
    }
}
