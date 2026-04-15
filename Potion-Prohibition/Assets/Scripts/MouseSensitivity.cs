using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    [System.Serializable]
    public class MouseData
    {
        public float sens = 4.50f;
    }

    public MouseData playerData = new MouseData();

    public Slider MouseSensSlider;
    private float mouseSens;
    public GameObject TitleCanvas;
    public GameObject SettingsCanvas;
    public TextMeshProUGUI text;

    private void Start()
    {
        ReadJson();

        MouseSensSlider.value = mouseSens == 4.50f ? 4.50f : mouseSens;
    }

    private void Update()
    {
        text.text = MouseSensSlider.value.ToString("F2");
    }

    public void WriteJson()
    {
        playerData.sens = mouseSens;

        string stringOutput = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/MouseData.json", stringOutput);
    }

    public void ReadJson()
    {
        string filepath = Application.persistentDataPath + "/MouseData.json";
        string playerDataRead = System.IO.File.ReadAllText(filepath);

        playerData = JsonUtility.FromJson<MouseData>(playerDataRead);

        mouseSens = playerData.sens;
    }

    public void changeTheCanvasToTitle()
    {
        mouseSens = MouseSensSlider.value;
        WriteJson();
        TitleCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
    }
}
