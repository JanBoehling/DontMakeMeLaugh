using System.Collections;
using System.Reflection;
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
        OnInteracted.AddListener((finger) => StartCoroutine(DrawCardCO(finger)));
    }

    private void Update()
    {
        if (!InAnimation) return;

        var pos = transform.position;
        pos.y = 0.825f;

        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    private IEnumerator DrawCardCO(Transform finger)
    {
        var arm = GameObject.Find("Player").transform.Find("Arm").GetChild(0);

        //transform.position = new Vector3(finger.position.x, transform.position.y, finger.position.z);
        //transform.SetParent(finger);

        arm.GetComponent<Animator>().SetTrigger("DoCard");

        yield return new WaitForSeconds(maxHandDistance);

        //transform.SetParent(null);

        float duration = 1.5f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, selectedCardPosition.position, elapsedTime / duration);

            var pos = transform.position;
            pos.y = 0.847f;
            transform.position = pos;
            transform.SetPositionAndRotation(pos, Quaternion.identity);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
