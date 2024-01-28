using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Card : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteracted { get; } = new();
    public bool InAnimation { get; set; }

    [SerializeField] private Transform arm;

    private Vector3 self;
    private Vector3 target;

    private void Awake()
    {
        OnInteracted.AddListener(() => StartCoroutine(RotateHandToTarget(arm, target)));
    }

    private void Update()
    {
        if (!InAnimation) return;

        var pos = transform.position;
        pos.y = 0.825f;

        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    private IEnumerator RotateHandToTarget(Transform arm, Vector3 target)
    {
        transform.Rotate(Vector3.up, 0f);
        yield return null;
    }
}
