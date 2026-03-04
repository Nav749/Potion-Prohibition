using UnityEngine;

public class CanvasSwap : MonoBehaviour
{
    public GameObject TitleCanvas;
    public GameObject SettingsCanvas;

    public void changeTheCanvasToSettings()
    {
        TitleCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void changeTheCanvasToTitle()
    {
        TitleCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
    }
}
