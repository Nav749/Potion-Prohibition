using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameManager.Instance.PlayerGO.transform;
    }

    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
