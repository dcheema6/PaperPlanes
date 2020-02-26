using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private CanvasGroup fade;
    private float fadeInSpeed = 0.33f;

    public RectTransform menuContainer;
    public Transform levelPanel;
    public Transform colorPanel;
    public Transform trailPanel;

    public Text colorButtonText;
    public Text trailButtonText;
    public Text goldText;

    private MenuCamera mCam;

    private int[] colorCost = new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 50 };
    private int[] trailCost = new int[] { 0, 5, 20, 30, 40, 50, 60, 70, 80, 100 };
    private int selectedColorIndex;
    private int selectedTrailIndex;
    private int activeColorIndex;
    private int activeTrailIndex;

    private Vector3 menuPosition;

    private GameObject currTrail;

    public AnimationCurve levelEnterZoomCurve;
    private bool isLevelEnter = false;
    private float zoomTime = 3.0f;
    private float zoomTransition;

    private Texture prevTrail;
    private GameObject lastPreviewObject;

    public Transform trailPreviewObject;
    public RenderTexture trailPreviewTexture;

    
    void Start()
    {
        // Grab the only one MenuCamera in the scene
        mCam = FindObjectOfType<MenuCamera>();
        // Grab the only one CanvasGroup in the scene
        fade = FindObjectOfType<CanvasGroup>();
        // Start with a white screen
        fade.alpha = 1;

        MoveCameraTo(GameManager.Instance.menu);
        UpdateGoldText();    
        InitLevelPanel();
        InitShop();

        // Set Player prefs for color & trail
        OnColorSelect(SaveManager.Instance.game.activeColor);
        SetColor(SaveManager.Instance.game.activeColor);

        OnTrailSelect(SaveManager.Instance.game.activeTrail);
        SetTrail(SaveManager.Instance.game.activeTrail);

        colorPanel.GetChild(SaveManager.Instance.game.activeColor).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        trailPanel.GetChild(SaveManager.Instance.game.activeTrail).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    }

    void Update()
    {
        // Fade-in
        fade.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        // Menu navigation
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, menuPosition, 0.1f);

        // Entering level zoom
        if (isLevelEnter)
            DoLevelTranstion();
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

            Image img = t.GetComponent<Image> ();
            if (i <= SaveManager.Instance.game.levelCompleted)
            {
                if (i == SaveManager.Instance.game.levelCompleted)
                    img.color  = Color.white;
                else
                    img.color = Color.green;
            }
            else
            {
                b.interactable = false;
                img.color = Color.grey;
            }
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

            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsColorOwned(i)
                ? GameManager.Instance.planeColors[currInd]
                : Color.Lerp(GameManager.Instance.planeColors[currInd], new Color(0,0,0,1), 0.3f);

            i++;
        }

        i = 0;
        foreach (Transform t in trailPanel)
        {
            int currInd = i;
            Button b = t.GetComponent<Button> ();
            b.onClick.AddListener(() => OnTrailSelect(currInd));
            
            RawImage img = t.GetComponent<RawImage>();
            img.color = SaveManager.Instance.IsTrailOwned(i)
                ? GameManager.Instance.planeColors[currInd]
                : Color.Lerp(GameManager.Instance.planeColors[currInd], new Color(0,0,0,1), 0.3f);

            i++;
        }

        prevTrail = trailPanel.GetChild(SaveManager.Instance.game.activeTrail).GetComponent<RawImage>().texture;
    }

    private void DoLevelTranstion()
    {
        // Add to the zoomTranstion
        zoomTransition += (1/zoomTime)*Time.deltaTime;
        // Change scale using animation curve
        menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, levelEnterZoomCurve.Evaluate(zoomTransition));
        // Change the desired menu position, so it can follow the scale up
        // This zooms in the center
        Vector3 desiredPostion = menuPosition * 5;
        // This adds to the specific postion on the level canvas
        RectTransform rt = levelPanel.GetChild(GameManager.Instance.currLevel).GetComponent<RectTransform>();
        desiredPostion -= rt.localPosition * 5;
        // ovveride previoys menu position update
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuPosition, desiredPostion, levelEnterZoomCurve.Evaluate(zoomTransition));

        // Fade to white screen, overriding previous fade update
        fade.alpha = zoomTransition;

        // are animation complete
        if (zoomTransition >= 1)
        {
            // Enter the level
            SceneManager.LoadScene("Game");
        }
    }

    private void MoveCameraTo(int mInd)
    {
        NavigateTo(mInd);
        menuContainer.anchoredPosition3D = menuPosition;
    }

    private void NavigateTo(int mInd)
    {
        switch (mInd)
        {
            // Main menu
            default:
            case 0:
                menuPosition = Vector3.zero;
                mCam.NavigateToMainMenu();
                break;
            // Play menu
            case 1:
                menuPosition = Vector3.right * 1280;
                mCam.NavigateToLevel();
                break;
            // Shop menu
            case 2:
                menuPosition = Vector3.left * 1280;
                mCam.NavigateToShop();
                break;
        }
    }

    private void SetColor(int ind)
    {
        activeColorIndex = ind;
        SaveManager.Instance.game.activeColor = ind;
        GameManager.Instance.planeMaterial.color = GameManager.Instance.planeColors[ind];
        colorButtonText.text = "Current";
        SaveManager.Instance.Save();
    }

    private void SetTrail(int ind)
    {
        activeTrailIndex = ind;
        SaveManager.Instance.game.activeTrail = ind;
        
        if (currTrail != null)
        {
            Destroy(currTrail);
        }
        currTrail = Instantiate(GameManager.Instance.planeTrails[ind]) as GameObject;
        currTrail.transform.SetParent(FindObjectOfType<MenuPlane>().transform);
        // Fix scaling and rotation issues
        currTrail.transform.localPosition = Vector3.zero;
        currTrail.transform.localRotation = Quaternion.Euler(0,0,90);
        currTrail.transform.localScale = Vector3.one * 0.01f;
        
        trailButtonText.text = "Current";
        SaveManager.Instance.Save();
    }

    private void UpdateGoldText()
    {
        goldText.text = SaveManager.Instance.game.gold.ToString();
    }

    // Buttton Controls
    public void OnBack()
    {
        NavigateTo(0);
    }

    public void OnPlay()
    {
        NavigateTo(1);
    }

    public void OnShop()
    {
        NavigateTo(2);
    }

    private void OnLevelSelect(int i)
    {
        GameManager.Instance.currLevel = i;
        isLevelEnter = true;
    }
    
    private void OnColorSelect(int i)
    {
        // If already selected, exit
        if (selectedColorIndex == i)
            return;
        
        // Highlight icon
        colorPanel.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        // Previous one to normal scale
        colorPanel.GetChild(selectedColorIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedColorIndex = i;
        if (SaveManager.Instance.IsColorOwned(i))
        {
            if (activeColorIndex == i)
                colorButtonText.text = "Current";
            else
                colorButtonText.text = "Select";
        }
        else
        {
            colorButtonText.text = "Buy: " + colorCost[i].ToString();
        }
    }
    
    private void OnTrailSelect(int i)
    {        
        // If already selected, exit
        if (selectedTrailIndex == i)
            return;

        // Preview trail
        // Get the preview image
        trailPanel.GetChild(selectedTrailIndex).GetComponent<RawImage>().texture = prevTrail;
        // Keep new preview image
        prevTrail = trailPanel.GetChild(i).GetComponent<RawImage>().texture;
        // Set new trail preview to secondary camera
        trailPanel.GetChild(i).GetComponent<RawImage>().texture = trailPreviewTexture;

        if (lastPreviewObject != null)
            Destroy(lastPreviewObject);
        GameObject ptrail = GameManager.Instance.planeTrails[i];
        ptrail.transform.localRotation = Quaternion.Euler(90,0,0);
        lastPreviewObject = GameObject.Instantiate(ptrail) as GameObject;
        lastPreviewObject.transform.SetParent(trailPreviewObject);
        lastPreviewObject.transform.localPosition = Vector3.zero;

        // Highlight icon
        trailPanel.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        // Previous one to normal scale
        trailPanel.GetChild(selectedColorIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedTrailIndex = i;
        if (SaveManager.Instance.IsTrailOwned(i))
        {
             if (activeTrailIndex == i)
                trailButtonText.text = "Current";
            else
                trailButtonText.text = "Select";
        }
        else
        {
            trailButtonText.text = "Buy: " + trailCost[i].ToString();
        }
    }

    public void OnColorSet()
    {
        if (SaveManager.Instance.IsColorOwned(selectedColorIndex))
        {
            SetColor(selectedColorIndex);
        }
        else
        {
            if (SaveManager.Instance.BuyColor(selectedColorIndex, colorCost[selectedColorIndex]))
            {
                SetColor(selectedColorIndex);
                colorPanel.GetChild(selectedColorIndex).GetComponent<Image>().color = GameManager.Instance.planeColors[selectedColorIndex];
                UpdateGoldText();
            }
            else
            {
                Debug.Log("Not enough gold");
            }
        }
    }

    public void OnTrailSet()
    {
        if (SaveManager.Instance.IsTrailOwned(selectedTrailIndex))
        {
            SetTrail(selectedTrailIndex);
        }
        else
        {
            if (SaveManager.Instance.BuyTrail(selectedTrailIndex, trailCost[selectedTrailIndex]))
            {
                SetTrail(selectedTrailIndex);
                trailPanel.GetChild(selectedTrailIndex).GetComponent<RawImage>().color = Color.white;
                UpdateGoldText();
            }
            else
            {
                Debug.Log("Not enough gold");
            }
        }
    }
}
