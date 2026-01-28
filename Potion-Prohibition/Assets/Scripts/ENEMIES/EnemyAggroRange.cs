using UnityEngine;

public class EnemyAggroRange : MonoBehaviour
{
    public GameObject enemyToTrigger;
    private EnemyTest enemyAggro;
    private void OnTriggerEnter(Collider other)
    {
        if (enemyToTrigger.GetComponent<EnemyTest>() != null && other.GetComponent<playerMovement>() != null)
        {
            enemyAggro = enemyToTrigger.GetComponent<EnemyTest>();
            enemyAggro.isAggroed = true;
        }
    }

}
