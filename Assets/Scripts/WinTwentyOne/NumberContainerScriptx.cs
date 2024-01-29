using System.Collections;
using UnityEngine;

public class NumberContainerScriptx : MonoBehaviour
{
    //[SerializeField] private GameObject[] GameObjectCards;
    [SerializeField] private Sprite[] SpriteCards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Material[] cardMaterials;

    //public GameObject GetCardPrefab(int drawnNumber) => GameObjectCards[drawnNumber];
    public Sprite GetCardUI(int drawnNumber) => SpriteCards[drawnNumber];
    public GameObject GetCardPrefab(int number)
    {
        var card = Instantiate(cardPrefab);
        card.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[number];
        return card;
    }
}
