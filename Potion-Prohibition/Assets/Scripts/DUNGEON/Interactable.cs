using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject[] spawnableObjects;
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] private AudioClip sfx;
    private AudioSource sfxSource;

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        sfxSource.clip = sfx;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            sfxSource.Play();

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
