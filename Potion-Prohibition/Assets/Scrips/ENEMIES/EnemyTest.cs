using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float testEnemyHealth;

    private void Update()
    {
        if(testEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
