using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject[] spawnableObjects;
    [SerializeField] Transform[] SpawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            foreach(GameObject item in spawnableObjects)
            {
                foreach (Transform t in SpawnPoints)
                {
                    Instantiate(item, t.position, Quaternion.identity, this.transform.parent.parent.parent);
                }
            }
            Destroy(this.transform.parent.parent.gameObject);
        }
    }
}
