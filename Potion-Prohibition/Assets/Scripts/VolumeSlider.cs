using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [System.Serializable]
    public class VolumeData
    {
        public float sfx = 100;
        public float music = 100;
    }

    public VolumeData playerData = new VolumeData();

    public AudioMixer theMixer;
    public Slider volumeSlider;
    public string volumeLabel;
    private float sfxValue = 100;
    private float musicVolume = 100;
    private float volumeValue;
    public bool isMusic;
    public GameObject TitleCanvas;
    public GameObject SettingsCanvas;

    public void WriteJson()
    {
        playerData.sfx = sfxValue;
        playerData.music = musicVolume;

        string stringOutput = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/VolumeData.json", stringOutput);
    }

    public void ReadJson()
    {
        string filepath = Application.persistentDataPath + "/VolumeData.json";
        string playerDataRead = System.IO.File.ReadAllText(filepath);

        playerData = JsonUtility.FromJson<VolumeData>(playerDataRead);

        sfxValue = playerData.sfx;
        musicVolume = playerData.music;
    }

    private void Start()
    {
        ReadJson();

        volumeValue = isMusic ? musicVolume : sfxValue;
        volumeSlider.value = volumeValue == 100 ? 100 : volumeValue;
        volumeValue = volumeSlider.value;
    }

    public void SetSound(float value)
    {
        float volumeInDb = Mathf.Log10(value / 100) * 20;
        theMixer.SetFloat(volumeLabel, volumeInDb);
    }

    public void changeTheCanvasToTitle()
    {
        if (isMusic)
        {
            musicVolume = volumeSlider.value;
        }
        else
        {
            sfxValue = volumeSlider.value;
        }
        WriteJson();
        TitleCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
    }
}
