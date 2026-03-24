using UnityEngine;

public class NewtDeAggroer : MonoBehaviour
{
    public EnemyMelee Newty;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Newty.meleeIsAggroed = false;
        }
    }
}
