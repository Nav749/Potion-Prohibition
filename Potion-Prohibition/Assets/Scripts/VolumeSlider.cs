using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer theMixer;

    public void SetSound(float volume)
    {
        theMixer.SetFloat("MyExposedParam", volume);
    }
}
