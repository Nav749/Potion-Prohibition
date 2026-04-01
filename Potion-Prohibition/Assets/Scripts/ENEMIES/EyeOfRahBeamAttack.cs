using System.Collections;
using UnityEngine;

public class EyeOfRahBeamAttack : MonoBehaviour
{
    public LayerMask noCollidePlease;
    private LineRenderer lr;
    public float beamLength = 50f;
    public float beamDuration = 2f;
    public int beamDmg = 0;
    public GameObject player;
    private bool BeamOff = true;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        if (player == null)
        {
            player = GameManager.Instance.PlayerGO;
        }
    }

    public void FireBeam()
    {
        StartCoroutine(BeamSequence());
    }

    IEnumerator BeamSequence()
    {
        lr.enabled = true;

        float timeElapsed = 0;
        while (timeElapsed < beamDuration)
        {
            UpdateBeam();
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        BeamOff = true;
        lr.enabled = false;
    }

    void UpdateBeam()
    {
        lr.SetPosition(0, transform.position);

        RaycastHit hit;

        if (BeamOff)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, beamLength, noCollidePlease))
            {
                lr.SetPosition(1, hit.point);
                if (hit.collider.GetComponent<playerMovement>() != null)
                {
                    player.GetComponent<playerHealth>().TakeDamage(beamDmg);
                    BeamOff = false;
                }
            }
            else
            {
                lr.SetPosition(1, transform.position + transform.forward * beamLength);
            }
        }
    }
}
