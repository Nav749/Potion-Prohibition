using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject[] spawnableObjects;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            foreach(GameObject item in spawnableObjects)
            {
                Instantiate(item, this.transform.position, Quaternion.identity, this.transform.parent.parent.parent);
            }
            Destroy(this.transform.parent.parent.gameObject);
        }
    }
}
