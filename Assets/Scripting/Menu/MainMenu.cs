using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject[] mainMenuElements;

    public float fadeInStartingTime = 0f;
    public float fadeInDuration = 1.5f;
    
    private void FadeInMenu()
    {
        StartCoroutine(Fade.FadeCoroutine(fadeInDuration, fadeInStartingTime, mainMenuElements, true));
    }

    void Start()
    {
        FadeInMenu();
    }
}
