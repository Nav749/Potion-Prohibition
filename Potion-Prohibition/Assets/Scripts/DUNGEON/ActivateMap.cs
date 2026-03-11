using UnityEngine;

public class ActivateMap : MonoBehaviour
{
    [SerializeField] GameObject Map;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && GameManager.Instance.currentLevelName == "Dungeon")
        {
            Map.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Map.SetActive(false);
        }
    }
}
