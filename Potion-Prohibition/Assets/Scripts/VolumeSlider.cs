using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer theMixer;
    public Slider volumeSlider;

    public void SetSound(float value)
    {
        float volumeInDb = Mathf.Log10(value) * 20;
        theMixer.SetFloat("MasterAudioParam", volumeInDb);
    }
}
