using UnityEngine;

public class EnemyAggroRange : MonoBehaviour
{
    public GameObject enemyToTrigger;
    private EnemyTest rangedEnemyAggro;
    private EnemyMelee meleeEnemyAggro;
    private void OnTriggerEnter(Collider other)
    {
        if (enemyToTrigger.GetComponent<EnemyTest>() != null && other.GetComponent<playerMovement>() != null)
        {
            rangedEnemyAggro = enemyToTrigger.GetComponent<EnemyTest>();
            rangedEnemyAggro.rangedIsAggroed = true;
        }

        if (enemyToTrigger.GetComponent<EnemyMelee>() != null && other.GetComponent<playerMovement>() != null)
        {
            meleeEnemyAggro = enemyToTrigger.GetComponent<EnemyMelee>();
            meleeEnemyAggro.meleeIsAggroed = true;
        }
    }

}
