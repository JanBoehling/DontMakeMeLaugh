using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardStackGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    public List<GameObject> StackedCards { get; private set; } = new();

    public void GenerateCardStack(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var card = Instantiate(cardPrefab, transform.position + i * 0.001f * Vector3.up, Quaternion.identity, transform);
            Destroy(card.GetComponent<Card>());
            Destroy(card.GetComponent<Collider>());
            StackedCards.Add(card);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CardStackGenerator))]
public class CardStackGeneratorEditor : Editor
{
    private int count = 0;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        count = EditorGUILayout.IntField("Card Amount", count);
        if (GUILayout.Button("Build Stack")) ((CardStackGenerator)target).GenerateCardStack(count);
        if (GUILayout.Button("Destroy all")) DestroyAllChildren(((CardStackGenerator)target).transform);
        serializedObject.ApplyModifiedProperties();
    }

    public void DestroyAllChildren(Transform parent, int index = -1)
    {
        if (index >= 0)
        {
            DestroyImmediate(parent.GetChild(index).gameObject);
            return;
        }

        int childCount = parent.childCount;

        var children = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = parent.GetChild(i).gameObject;
        }

        foreach (var child in children)
        {
            DestroyImmediate(child);
        }
    }
}
#endif
