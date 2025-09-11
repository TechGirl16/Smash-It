using UnityEngine;
using System.Collections;

public class BallPowerups : MonoBehaviour
{
    private Vector3 originalScale;
    private Coroutine activeRoutine;

    void Start()
    {
        originalScale = transform.localScale; // Store the ball's starting size
    }

    public void ApplyBig(float factor, float duration)
    {
        if (activeRoutine != null) StopCoroutine(activeRoutine);
        activeRoutine = StartCoroutine(BigRoutine(factor, duration));
    }

    private IEnumerator BigRoutine(float factor, float duration)
    {
        // Grow uniformly in 2D (X and Y)
        Vector3 bigScale = originalScale * factor;
        transform.localScale = bigScale;

        yield return new WaitForSeconds(duration);

        // Revert to original size
        transform.localScale = originalScale;
        activeRoutine = null;
    }
}