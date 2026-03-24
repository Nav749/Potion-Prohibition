using UnityEngine;

public class DewDropperDeAggro : MonoBehaviour
{
    public EnemyTest DewdropperDown;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DewdropperDown.rangedIsAggroed = false;
        }
    }
}
