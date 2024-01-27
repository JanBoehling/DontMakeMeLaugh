using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContainerScriptx : MonoBehaviour
{
    public GameObject[] GameObjectCards;
    public GameObject correctGameObject;

    public void GettingTheCorrectTexture(int drawnNumber)
    {
        correctGameObject = GameObjectCards[drawnNumber];
    }
}
