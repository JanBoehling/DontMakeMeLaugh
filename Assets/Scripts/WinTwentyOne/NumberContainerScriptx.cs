using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContainerScriptx : MonoBehaviour
{
    public Material[] Material;
    public Material correctMaterial;

    public void GettingTheCorrectTexture(int drawnNumber)
    {
        correctMaterial = Material[drawnNumber -1];
    }
}
