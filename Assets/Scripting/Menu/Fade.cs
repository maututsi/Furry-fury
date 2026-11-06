using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fade
{
    public static IEnumerator FadeCoroutine(float fadeDuration, float startingTime, GameObject[] elements, bool fadeIn)
    {
        yield return new WaitForSeconds(startingTime);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            foreach (GameObject element in elements)
            {
                element.TryGetComponent<CanvasGroup>(out CanvasGroup elementCG);
                elementCG.alpha = fadeIn ? elapsedTime / fadeDuration : 1 - elapsedTime / fadeDuration;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
