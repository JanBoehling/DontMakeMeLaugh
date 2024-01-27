using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContainerScriptx : MonoBehaviour
{
    public Texture[] textures;
    public Texture correctTexture;

    public void GettingTheCorrectTexture(int drawnNumber)
    {
        correctTexture = textures[drawnNumber];
    }
}
