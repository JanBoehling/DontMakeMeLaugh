using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCard : MonoBehaviour, IInteractable
{
    public CardSO CardSO;
    public TheKingsGame Game;
    public int Index;

    public UnityEvent OnInteracted => onInteracted;

    UnityEvent<Transform> IInteractable.OnInteracted => throw new System.NotImplementedException();

    private UnityEvent onInteracted;

    private void Start()
    {
        onInteracted = new UnityEvent();
        onInteracted.AddListener(CardWasPressed);
    }

    /// <summary>
    /// Call if Card was hit by Raycast
    /// </summary>
    public void CardWasPressed()
    {
        Game.PlayCard(Index, true);
    }
}
