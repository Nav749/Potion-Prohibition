using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalToLevel : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<playerSpellShoot>() != null)
        {
            //other.GetComponent<playerSpellShoot>().tavernNeutral = true;
            GameManager.Instance.LoadLevels();
        }
    }
}
