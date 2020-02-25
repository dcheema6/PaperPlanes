using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    public RectTransform menuContainer;
    public Transform levelPanel;

    public Transform colorPanel;
    public Transform trailPanel;

    private Vector3 menuPosition;
    
    void Start()
    {
        // Grab the only one CanvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start with a white screen
        fadeGroup.alpha = 1;
    
        InitLevelPanel();
        InitShop();
    }

    void Update()
    {
        // Fade-in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        // Menu navigation
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, menuPosition, 0.1f);
    }

    private void InitLevelPanel()
    {
        // Check references
        if (levelPanel == null)
            Debug.Log("Level panel refence(s) has not been assigned in inspector");

        // Add event listerners to every child of color/trail panel
        int i = 0;
        foreach (Transform t in levelPanel)
        {
            int currInd = i;
            Button b = t.GetComponent<Button> ();
            b.onClick.AddListener(() => OnLevelSelect(currInd));
            i++;
        }
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

    private void NavigateTo(int mInd)
    {
        switch (mInd)
        {
            // Main menu
            default:
            case 0:
                menuPosition = Vector3.zero;
                break;
            // Play menu
            case 1:
                menuPosition = Vector3.right * 1280;
                break;
            // Shop menu
            case 2:
                menuPosition = Vector3.left * 1280;
                break;
        }
    }

    // Buttton Controls
    public void OnBack()
    {
        NavigateTo(0);
        Debug.Log("Back");
    }

    public void OnPlay()
    {
        NavigateTo(1);
        Debug.Log("Play");
    }

    public void OnShop()
    {
        NavigateTo(2);
        Debug.Log("Shop");
    }

    private void OnLevelSelect(int i)
    {
        Debug.Log("Selecting trail: " + i);
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
