using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InitialScreen : MonoBehaviour
{
    public GameObject[] initialScreenElements;

    public GameObject nextScreen;

    public TextMeshProUGUI flashingText;
    public float flashDuration = 3f;
    public float waveHeight = 1f;

    public float fadeInStartingTime = 1.5f;
    public float fadeInDuration = 3f;

    public float fadeOutStartingTime = 0f;
    public float fadeOutDuration = 2f;

    private float elapsedTime = 0f;

    private void UpdateColor(TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, (waveHeight / 2) * Mathf.Cos(Mathf.PI * elapsedTime / flashDuration) + 0.5f);
        elapsedTime += Time.deltaTime;
    }

    private void FadeInInitialScreen()
    {
        StartCoroutine(Fade.FadeCoroutine(fadeInDuration, fadeInStartingTime, initialScreenElements, true));
    }

    private void FadeOutInitialScreen()
    {
        StartCoroutine(Fade.FadeCoroutine(fadeOutDuration, fadeOutStartingTime, initialScreenElements, false));
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeOutInitialScreen();

            nextScreen.SetActive(true);
        }
    }

    void Start()
    {
        FadeInInitialScreen();
    }

    void Update()
    {
        UpdateColor(flashingText);
        CheckInput();
    }
}
