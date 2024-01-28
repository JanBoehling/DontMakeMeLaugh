using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCard : MonoBehaviour, IInteractable
{
    public CardSO CardSO;
    public TheKingsGame Game;
    public int Index;

    public UnityEvent<Transform> OnInteracted => onInteracted;

    //UnityEvent IInteractable.OnInteracted => throw new System.NotImplementedException();

    private UnityEvent<Transform> onInteracted;

    private void Start()
    {
        onInteracted = new UnityEvent<Transform>();
        onInteracted.AddListener(CardWasPressed);
    }

    /// <summary>
    /// Call if Card was hit by Raycast
    /// </summary>
    public void CardWasPressed(Transform transform)
    {
        Game.PlayCard(Index, true);
    }
}
