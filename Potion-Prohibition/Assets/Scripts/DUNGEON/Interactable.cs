using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject[] spawnableObjects;
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] private AudioClip sfx;
    private AudioSource sfxSource;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Collider collider;
    [SerializeField] private Collider trigger;

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        sfxSource.clip = sfx;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            sfxSource.Play();

            if (spawnableObjects.Length > 0)
            {
                int randomNum = Random.Range(0, 1);
                Instantiate(spawnableObjects[randomNum], SpawnPoints[randomNum].position, Quaternion.identity, this.transform.parent.parent.parent);
            }


            foreach (GameObject item in spawnableObjects)
            {
                foreach (Transform t in SpawnPoints)
                {
                    Instantiate(item, t.position, Quaternion.identity, this.transform.parent.parent.parent);
                }
            }


            hide();
            Invoke("distory", 3);

        }
    }

    private void hide()
    {
        meshRenderer.enabled = false;
        collider.enabled = false;
        trigger.enabled = false;



    }
    private void distory()
    {
        Destroy(this.transform.parent.parent.gameObject);
    }
}
