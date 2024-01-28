using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AttackSequence : MonoBehaviour
{
    [SerializeField] private GameObject axeOnTable = default;
    [SerializeField] private GameObject axeInHand = default;

    [SerializeField] private GameObject redCanvas = default;
    [SerializeField] private GameObject gameOverCanvas = default;

    [SerializeField] private GameObject arm = default;
    [SerializeField] private GameObject severedHand = default;

    private Animator animator = default;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlaySequence()
    {
        // Beil on table deactivated, beil in hand activated
        axeOnTable.SetActive(false);
        axeInHand.SetActive(true);

        // Animation played
        animator.SetTrigger("DoAttack");

        // After two seconds, activate red canvas, deactivate after .5 seconds
        StartCoroutine(DeathCanvasCO());
    }

    private IEnumerator DeathCanvasCO()
    {
        yield return new WaitForSeconds(2f);
        redCanvas.SetActive(true);

        yield return new WaitForSeconds(.5f);
        redCanvas.SetActive(false);

        // Replace arm with severed arm object
        arm.SetActive(false);
        severedHand.SetActive(true);

        // Activate game over canvas
        gameOverCanvas.SetActive(true);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AttackSequence))]
public class AttackSequenceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        if (GUILayout.Button("Attack!")) ((AttackSequence)target).PlaySequence();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
