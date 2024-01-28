using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Card : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteracted { get; } = new();

    [SerializeField] private Transform arm;

    private Vector3 self;
    private Vector3 target;

    private void Awake()
    {
        OnInteracted.AddListener(() => StartCoroutine(RotateHandToTarget(arm, target)));
    }

    private IEnumerator RotateHandToTarget(Transform arm, Vector3 target)
    {
        transform.Rotate(Vector3.up, 0f);
        yield return null;
    }
}
