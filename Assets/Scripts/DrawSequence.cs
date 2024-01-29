using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrawSequence : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform finger;
    [SerializeField] private Transform cardStack;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlaySequence() => StartCoroutine(PlaySequenceCO());

    private IEnumerator PlaySequenceCO()
    {
        // Instantiate new card
        var topInStack = cardStack.GetChild(cardStack.childCount - 1);
        var card = Instantiate(cardPrefab, topInStack.transform.position, Quaternion.identity);
        card.transform.SetParent(finger);
        card.transform.localScale *= .25f;
        var rotator = card.AddComponent<CardRotator>();
        rotator.InAnimation = true;

        // Trigger animation
        animator.SetTrigger("DoCard");

        // After x seconds, unparent card
        yield return new WaitForSeconds(.625f);

        card.transform.SetParent(null, true);
        rotator.InAnimation = false;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DrawSequence))]
public class DrawSequenceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        if (Application.isPlaying && GUILayout.Button("Draw card!")) ((DrawSequence)target).PlaySequence();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
