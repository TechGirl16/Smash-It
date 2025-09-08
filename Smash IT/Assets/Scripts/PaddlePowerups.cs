using UnityEngine;
using System.Collections;

public class PaddlePowerups : MonoBehaviour
{
    private Vector3 originalScale;
    private Coroutine activeRoutine;

    void Start()
    {
        originalScale = transform.localScale; // remember starting size
    }

    public void ApplyBig(float factor, float duration)
    {
        if (activeRoutine != null) StopCoroutine(activeRoutine);
        activeRoutine = StartCoroutine(BigRoutine(factor, duration));
    }

    private IEnumerator BigRoutine(float factor, float duration)
    {
        // Grow in height (Y)
        Vector3 bigScale = new Vector3(originalScale.x, originalScale.y * factor, originalScale.z);
        transform.localScale = bigScale;

        yield return new WaitForSeconds(duration);

        // Back to normal
        transform.localScale = originalScale;
        activeRoutine = null;
    }
}
