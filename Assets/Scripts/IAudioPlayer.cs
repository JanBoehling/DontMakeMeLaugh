using UnityEngine.Events;
using UnityEngine;

public interface IAudioPlayer
{
    public UnityEvent<string, GameObject> AudioPlayEvent { get; }
}