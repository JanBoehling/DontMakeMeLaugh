using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrawSequence : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform hand;

    private CardStackGenerator stack;
    private Animator animator;

    private void Awake()
    {
        stack = FindAnyObjectByType<CardStackGenerator>();
        animator = GetComponentInChildren<Animator>();
    }

    public void PlaySequence()
    {
        var topInStack = stack.transform.GetChild(stack.transform.childCount - 1);
        var card = Instantiate(cardPrefab, topInStack.transform.position, Quaternion.identity, hand);
        card.GetComponent<Card>().InAnimation = true;
        //card.transform.localScale = Vector3.one * 0.05f;
        //var pos = card.transform.position;
        //pos.y = 0.825f;
        //card.transform.position = pos;

        // Trigger animation
        animator.SetTrigger("DoCard");

        StartCoroutine(PlaySequenceCO(card.transform));
    }

    private IEnumerator PlaySequenceCO(Transform card)
    {
        yield return new WaitForSeconds(.625f);

        card.SetParent(null);
        card.GetComponent<Card>().InAnimation = false;
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
        if (GUILayout.Button("Draw card!")) ((DrawSequence)target).PlaySequence();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
