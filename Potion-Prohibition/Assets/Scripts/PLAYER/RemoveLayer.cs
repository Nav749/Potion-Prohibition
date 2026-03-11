using UnityEngine;

public class RemoveLayer : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.SetActive(GameManager.Instance.PlayerGO.GetComponent<PlayerLock>().isCrafting);
    }
}
