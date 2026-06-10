using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] public float animationDuration = 0.2f;

    private Coroutine scaleCoroutine;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = StartCoroutine(ScaleButton(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(ScaleButton(originalScale));
    }

    IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / animationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
