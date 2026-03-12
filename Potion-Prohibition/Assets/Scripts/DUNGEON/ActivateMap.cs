using UnityEngine;

public class ActivateMap : MonoBehaviour
{
    [SerializeField] GameObject Map;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && GameManager.Instance.currentLevelName == "Dungeon")
        {
            Map.SetActive(true);
            GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Map.SetActive(false);
            GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(false);
        }
    }
}
