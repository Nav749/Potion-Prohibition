using UnityEngine;

public class RangedEnemyAggroBubble : MonoBehaviour
{
    public GameObject EnemyToIndicate;

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.GetComponent<playerMovement>() != null)
        {
            EnemyToIndicate.GetComponent<EnemyTest>().rangedInrange = true;
            EnemyToIndicate.GetComponent<EnemyTest>().rangedIsAggroed = true;
        }

    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.GetComponent<playerMovement>() != null)
        {
            EnemyToIndicate.GetComponent<EnemyTest>().rangedInrange = false;
        }
    }
}
