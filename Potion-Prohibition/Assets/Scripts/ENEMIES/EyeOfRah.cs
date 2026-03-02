using System.Collections;
using UnityEngine;

public class EyeOfRah : MonoBehaviour
{
    public bool rahIsAggroed = false;

    public float rahEnemyHealth;

    public GameObject playerTargetForRah;

    public GameObject rahAttackPrefab;

    public Transform rahAttackSource;

    private Quaternion lookingRotatorRah;

    private Vector3 targetDirectionRah;

    private int ticker = 0;

    public int rangedEnemyAttackSpeed = 50;

    public int rangedEnemyBulletSpeed = 1;

    public Rigidbody enemyRB;

    private void Start()
    {
        playerTargetForRah = GameManager.Instance.PlayerGO;
    }

    private void Update()
    {
        if (rahEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        targetDirectionRah = (playerTargetForRah.transform.position - transform.position).normalized;
        lookingRotatorRah = Quaternion.LookRotation(targetDirectionRah);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRah, 85);

        if (rahIsAggroed == true)
        {
            ticker += 1;

            if (ticker >= rangedEnemyAttackSpeed * 60)
            {
                StartCoroutine(RahBeam());
            }
        }
    }

    IEnumerator RahBeam()
    {
        GameObject EnemyAttackInRange = Instantiate(rahAttackPrefab, rahAttackSource.position, transform.rotation);
        EnemyAttackInRange.GetComponent<Rigidbody>().AddForce(rahAttackSource.forward * rangedEnemyBulletSpeed, ForceMode.Impulse);
        ticker = 0;
        yield return null;
    }

}
