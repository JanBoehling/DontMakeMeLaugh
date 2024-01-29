using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Card : MonoBehaviour, IInteractable
{
    public UnityEvent<Transform> OnInteracted { get; } = new();
    public bool InAnimation { get; set; }

    [SerializeField] private float maxHandDistance = .5f;
    [SerializeField] private Transform selectedCardPosition;

    private void Awake()
    {
        OnInteracted.AddListener((finger) => StartCoroutine(DrawCardCO(GameObject.Find("Arm").GetComponentInChildren<Animator>(), finger, selectedCardPosition, null, 0, .5f, .25f)));
        OnInteracted.AddListener((_) => ClownVoicelinePlayer.Instance.ResetTimer());
    }

    private void Update()
    {
        if (!InAnimation) return;

        var pos = transform.position;
        pos.y = 0.825f;

        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    public IEnumerator DrawCardCO(Animator anim, Transform finger, Transform cardHoldingPoint, NumberContainerScriptx numberContainer, int numberOnCard, float parentWaitDuration, float unparentWaitDuration)
    {
        anim.SetTrigger("DoCard");

        yield return new WaitForSeconds(parentWaitDuration);

        var cardPrefab = numberContainer.GetCardPrefab(numberOnCard);
        var itemObject = Instantiate(cardPrefab, finger.position, Quaternion.identity, finger);
        itemObject.transform.localScale = new(.05f, .05f, .05f);

        // After x more seconds, unparent card
        yield return new WaitForSeconds(unparentWaitDuration);
        itemObject.transform.SetParent(null);

        // Throw card to destination
        float duration = 1.5f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, cardHoldingPoint.position, elapsedTime / duration);

            var pos = transform.position;
            pos.y = 0.847f;
            transform.position = pos;
            transform.SetPositionAndRotation(pos, Quaternion.identity);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
