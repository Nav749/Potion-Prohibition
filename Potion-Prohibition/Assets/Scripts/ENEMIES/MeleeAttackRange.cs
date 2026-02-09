using UnityEngine;

public class MeleeAttackRange : MonoBehaviour
{
    public GameObject EnemyToIndicate;

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.GetComponent<playerMovement>() != null)
        {
            EnemyToIndicate.GetComponent<EnemyMelee>().meleeInRange = true;
        }

    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.GetComponent<playerMovement>() != null)
        {
            EnemyToIndicate.GetComponent<EnemyMelee>().meleeInRange = false;
        }
    }
}
