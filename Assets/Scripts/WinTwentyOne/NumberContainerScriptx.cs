using System.Collections;
using UnityEngine;

public class NumberContainerScriptx : MonoBehaviour
{
    [SerializeField] private GameObject[] GameObjectCards;

    public GameObject GetCardPrefab(int drawnNumber) => GameObjectCards[drawnNumber];
}
