using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpeakeasyBand : MonoBehaviour
{

    [SerializeField] List<AudioClip> songs;
    public AudioSource[] source = new AudioSource[2];
    private AudioClip newsong;
    private bool activeSource;
    bool isFading = false;
    IEnumerator musicTransition;

    void Awake()
    {
        int randint = Random.Range(0, 100) % songs.Count;
        newsong = songs[randint];
        PlayCrossfade(newsong);
    }

    void PlayCrossfade(AudioClip clip)
    {
        int nextSource = !activeSource ? 0 : 1;
        int currentSource = activeSource ? 0 : 1;

        if(clip == source[currentSource].clip)
        {
            return;
        }

        if(musicTransition != null)
        {
            StopCoroutine(musicTransition);
        }

        source[nextSource].clip = clip;
        source[nextSource].Play();

        musicTransition = Crossfade(20);
        StartCoroutine(musicTransition);
        
    }

    IEnumerator Crossfade(int time)
    {
        for (int i = 0; i < time + 1; i++)
        {
            source[0].volume = activeSource ? (time - i) * (1f / time) : (0 + i) * (1f / time);
            source[1].volume = !activeSource ? (time - i) * (1f / time) : (0 + i) * (1f / time);

            yield return new WaitForSeconds(0.1f);
        }

        source[activeSource ? 0 : 1].Stop();

        activeSource = !activeSource;
        musicTransition = null;
    }
}
