using UnityEngine;
using UnityEngine.Events;

public class HelpCardController : MonoBehaviour, IInteractable
{
    public UnityEvent<Transform> OnInteracted { get; } = new();
    [SerializeField] private GameObject helpCanvas;

    private void Awake()
    {
        OnInteracted.AddListener((_) =>
        {
            helpCanvas.SetActive(true);
        });
    }

    private void Update()
    {
        if (helpCanvas != null && helpCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape)) CloseCanvas(); 
    }

    public void CloseCanvas() => helpCanvas.SetActive(false);
}
