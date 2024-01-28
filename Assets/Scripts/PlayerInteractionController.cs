using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange = 10f;
    [SerializeField] private LayerMask interactionMask = -1;
    [SerializeField] private Transform fingerEnd;

    private Camera mainCam = null;

    private void Awake() => mainCam = Camera.main;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Interact();
    }

    private void Interact()
    {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        bool hasIntersect = Physics.Raycast(ray, out var hitInfo, interactionRange, interactionMask);

        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red, duration: 2f);

        if (!hasIntersect || !hitInfo.collider) return;

        if (hitInfo.transform.TryGetComponent<IInteractable>(out var interactable)) interactable.OnInteracted.Invoke(fingerEnd);
    }
}