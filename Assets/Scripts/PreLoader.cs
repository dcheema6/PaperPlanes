using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PreLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minLogoTime = 3.0f; // Minimum scene time

    private void Start()
    {
        // Grab the only one CanvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start with a white screen
        fadeGroup.alpha = 1;

        // Pre load the game
        // Here

        if (Time.time < minLogoTime)
            loadTime = minLogoTime;
        else
            loadTime = Time.time;
    }

    private void Update()
    {
        // Fade-in
        if (Time.time < minLogoTime)
            fadeGroup.alpha = 1 - Time.time;
        // Fade-out 
        else if (loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minLogoTime;
            if (fadeGroup.alpha >= 1)
            {
                EditorSceneManager.LoadScene("Menu");
            }
        }
    }
}
