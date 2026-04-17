using UnityEngine;

public class DewdropperThorns : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<playerHealth>())
        {
            other.GetComponent<playerHealth>().TakeDamage(1);
        }
    }
}
