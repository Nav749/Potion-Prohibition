using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalToLevel : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    [SerializeField] AudioClip portalsound;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource portalsoundSource;
    private void Start()
    {
        sfxSource.clip = sfx;
        portalsoundSource.clip = portalsound;
        portalsoundSource.Play();
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<playerSpellShoot>() != null)
        {
            //other.GetComponent<playerSpellShoot>().tavernNeutral = true;
            sfxSource.Play();
            GameManager.Instance.LoadLevels();
            other.GetComponent<playerMovement>().setJumpLock();
        }
    }
}
