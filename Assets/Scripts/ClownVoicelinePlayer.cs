using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ClownVoicelinePlayer : MonoBehaviour
{
    public static ClownVoicelinePlayer Instance { get; private set; }

    [SerializeField] private float voicelineDelay = 10f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private string[] voicelines;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool allowTalkAnim;

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

        if (allowTalkAnim) StartCoroutine(MoveMouthCO());
    }

    private IEnumerator MoveMouthCO()
    {
        enemy.transform.GetChild(0).GetComponent<Animator>().SetBool("DoTalk", true);
        yield return new WaitForSeconds(audioSource.clip.length);
        enemy.transform.GetChild(0).GetComponent<Animator>().SetBool("DoTalk", false);
    }

    public void ResetTimer() => elapsedTime = 0;
}
