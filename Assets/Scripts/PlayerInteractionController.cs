using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Interact();
    }

    private void Interact()
    {

    }
}