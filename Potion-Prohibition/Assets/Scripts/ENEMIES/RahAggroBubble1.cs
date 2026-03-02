using UnityEngine;

public class RahAggroBubble : MonoBehaviour
{
    public GameObject EnemyToIndicate;

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.GetComponent<playerMovement>() != null)
        {
            EnemyToIndicate.GetComponent<EyeOfRah>().rahIsAggroed = true;
        }
    }
}
