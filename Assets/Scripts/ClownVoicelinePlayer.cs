using UnityEngine;
using UnityEngine.Events;

public class ClownVoicelinePlayer : MonoBehaviour
{
    public static ClownVoicelinePlayer Instance { get; private set; }

    [SerializeField] private float voicelineDelay = 10f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private string[] voicelines;

    public UnityEvent<string, GameObject> AudioPlayEvent;

    private float elapsedTime = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (elapsedTime < voicelineDelay)
        {
            elapsedTime += Time.deltaTime;
            return;
        }

        elapsedTime = Random.Range(-50f, -10f);

        AudioPlayEvent?.Invoke(voicelines[Random.Range(0, voicelines.Length)], enemy);
    }

    public void ResetTimer() => elapsedTime = 0;
}
