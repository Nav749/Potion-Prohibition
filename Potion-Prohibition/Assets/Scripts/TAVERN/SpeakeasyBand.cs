using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpeakeasyBand : MonoBehaviour
{

    [SerializeField] List<AudioClip> songs;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = songs[0];
    }

    void Update()
    {
        
    }
}
