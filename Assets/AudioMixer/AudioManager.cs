using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _radio;
    [SerializeField]
    private GameObject _environment;
    [SerializeField]
    private GameObject _clown;
    [SerializeField]
    private List<AudioClip> _clips;
    [SerializeField]
    private List<string> _audioNames;

    private Dictionary<string, AudioClip> _audioClips;

    private void Start()
    {
        for (int i = 0; i < _clips.Count && i < _audioNames.Count; i++)
        {
            _audioClips.Add(_audioNames[i], _clips[i]);
        }

        // Sub to events
        _clown.GetComponent<Win21Game>().AudioPlayEvent.AddListener(OnPlayAudioSource);
        _clown.GetComponent<TheKingsGame>().PlayAudioEvent.AddListener(OnPlayAudioSource);
    }

    private void OnPlayAudioSource(string sourceName, GameObject source)
    {
        if (_audioClips.ContainsKey(sourceName))
        {
            if (source.GetComponent<AudioSource>() == null)
            {
                source.AddComponent<AudioSource>();
            }
            source.GetComponent<AudioSource>().clip = _audioClips[sourceName];
            source.GetComponent<AudioSource>().Play();
        }
    }
}
