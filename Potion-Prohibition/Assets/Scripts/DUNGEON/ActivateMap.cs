using UnityEngine;

public class ActivateMap : MonoBehaviour
{
    [SerializeField] GameObject Map;
    private bool mapUp = false;

    void Update()
    {
        if (!mapUp && Input.GetKeyDown(KeyCode.M) && GameManager.Instance.currentLevelName == "Dungeon")
        {
            mapUp = true;
            Map.SetActive(true);
            Time.timeScale = 0;
            GameManager.Instance.inMenu = true;
        }
        else if ((mapUp && Input.GetKeyDown(KeyCode.Escape)) || (mapUp && Input.GetKeyDown(KeyCode.M)))
        {
            mapUp = false;
            Map.SetActive(false);
            Time.timeScale = 1;
            Invoke("changeInMenu", 0.1f);
        }
    }

    private void changeInMenu() { GameManager.Instance.inMenu = false; }
}
