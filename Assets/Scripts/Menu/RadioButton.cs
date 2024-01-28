using UnityEngine;
using UnityEngine.Events;

public class RadioButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnButtonPressed;

    public UnityEvent OnInteracted => onInteracted;

    UnityEvent<Transform> IInteractable.OnInteracted => throw new System.NotImplementedException();

    private UnityEvent onInteracted;

    private void Start()
    {
        onInteracted = new UnityEvent();
        onInteracted.AddListener(Interaction);    
    }

    private void Interaction()
    {
        OnButtonPressed?.Invoke();
    }
}
