using UnityEngine;

public class CardRotator : MonoBehaviour
{
    public bool InAnimation { get; set; } = false;

    private void Update()
    {
        if (!InAnimation) return;

        var pos = transform.position;
        pos.y = -1.6144f;

        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
}
