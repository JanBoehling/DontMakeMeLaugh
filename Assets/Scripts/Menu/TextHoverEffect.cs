using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;



public class TextHoverEffect : MonoBehaviour
{
    private Animator animator;
    private TMP_Text textComponent;
    private Vector3 originalScale;
    private Vector3 hoverScale;

    void Start()
    {
        textComponent = gameObject.GetComponent<TMP_Text>();
        originalScale = transform.localScale;
        hoverScale = originalScale * 1.5f;
    }

    private IEnumerator ScaleIncrease(Vector3 targetScale)
    {
        float duration = 0.2f;
        float elapsedTime = 0F;

        while (elapsedTime < duration)

        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    public void OnPointerEnter()
    {
        StartCoroutine(ScaleIncrease(hoverScale));
    }

    public void OnPointerExit()
    {
        StartCoroutine(ScaleIncrease(originalScale));
    }
} 