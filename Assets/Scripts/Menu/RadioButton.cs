using UnityEngine;
using UnityEngine.Events;

public class RadioButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnButtonPressed;

    public UnityEvent<Transform> OnInteracted { get; } = new();

    private void Start()
    {
        OnInteracted.AddListener((_) => Interaction());    
    }

    private void Interaction()
    {
        OnButtonPressed?.Invoke();
    }
}
