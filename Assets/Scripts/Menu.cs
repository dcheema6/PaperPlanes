using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    public Transform colorPanel;
    public Transform trailPanel;
    
    void Start()
    {
        // Grab the only one CanvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start with a white screen
        fadeGroup.alpha = 1;
    
        // Add button on-click events to shop buttons
        InitShop();
    }

    void Update()
    {
        // Fade-in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }

    private void InitShop()
    {
        // Check references
        if (colorPanel == null || trailPanel == null)
            Debug.Log("Color/Trail panel refence(s) has not been assigned in inspector");

        // Add event listerners to every child of color/trail panel
        int i = 0;
        foreach (Transform t in colorPanel)
        {
            int currInd = i;
            Button b = t.GetComponent<Button> ();
            b.onClick.AddListener(() => OnColorSelect(currInd));
            i++;
        }

        i = 0;
        foreach (Transform t in trailPanel)
        {
            int currInd = i;
            Button b = t.GetComponent<Button> ();
            b.onClick.AddListener(() => OnTrailSelect(currInd));
            i++;
        }
    }

    // Buttton Controls
    public void OnPlay()
    {
        Debug.Log("Play");
    }

    public void OnExit()
    {
        Debug.Log("Exit");
    }
    
    private void OnColorSelect(int i)
    {
        Debug.Log("Selecting color: " + i);
    }
    
    private void OnTrailSelect(int i)
    {
        Debug.Log("Selecting trail: " + i);
    }

    public void OnColorSet()
    {
        Debug.Log("Set color");
    }

    public void OnTrailSet()
    {
        Debug.Log("Trail color");
    }
}
