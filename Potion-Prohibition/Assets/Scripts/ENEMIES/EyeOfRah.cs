using System.Collections;
using UnityEngine;

public class EyeOfRah : MonoBehaviour
{
    public bool rahIsAggroed = false;

    private bool rahRotation = true;

    public float rahEnemyHealth;

    public GameObject playerTargetForRah;

    public EyeOfRahBeamAttack beam;

    private Quaternion lookingRotatorRah;

    private Vector3 targetDirectionRah;

    private int ticker = 0;

    public int rangedEnemyAttackSpeed = 50;

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


        if (rahRotation == true)
        {
            targetDirectionRah = (playerTargetForRah.transform.position - transform.position).normalized;
            lookingRotatorRah = Quaternion.LookRotation(targetDirectionRah);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRah, 85);
        }

        if (rahIsAggroed == true)
        {
            ticker += 1;

            if (ticker >= rangedEnemyAttackSpeed * 60)
            {
                StartCoroutine(RahBeam());
                ticker = 0;
            }
        }
    }

    IEnumerator RahBeam()
    {
        rahRotation = false;
        rahIsAggroed = false;
        yield return new WaitForSeconds(1f);
        beam.FireBeam();
        yield return new WaitForSeconds(1f);
        rahRotation = true;
        rahIsAggroed = true;
    }

}
