using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioButton : MonoBehaviour
{
    public UnityEvent OnButtonPressed;

    private void OnCollisionEnter(Collision collision)
    {
        OnButtonPressed?.Invoke();
    }
}
