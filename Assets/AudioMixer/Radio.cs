using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> musicClips = new List<AudioClip>();

    private int musicClipIndex = 0;
    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicClips[musicClipIndex];
        musicSource.Play();
    }

    public void OnNextMusic()
    {
        musicSource.Stop();
        musicClipIndex++;
        if (musicClipIndex > musicClips.Count)
            musicClipIndex = 0;
        musicSource.clip = musicClips[musicClipIndex];
        musicSource.Play();
    }

    public void OnTurnDown()
    {
        musicSource.volume -= 10;
    }

    public void OnTurnUp()
    {
        musicSource.volume += 10;
    }
}