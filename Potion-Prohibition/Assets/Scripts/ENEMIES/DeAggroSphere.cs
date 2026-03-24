using UnityEngine;

public class DeAggroSphere : MonoBehaviour
{
    public EyeOfRah TheEye;

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<playerMovement>() != null)
        {
            TheEye.rahIsAggroed = false;
        }
    }
}
