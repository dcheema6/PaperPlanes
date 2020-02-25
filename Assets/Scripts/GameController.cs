using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private CanvasGroup fade;
    private float fadeInTime = 2;
    private bool gameStarted;

    void Start()
    {
        fade = FindObjectOfType<CanvasGroup>();
        fade.alpha = 1;
        gameStarted = false;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad <= fadeInTime)
            // Initial fade-in
            fade.alpha = 1 - (Time.timeSinceLevelLoad / fadeInTime);
        else if (!gameStarted)
        {
            // Ensure fade is gone
            fade.alpha = 0;
            gameStarted = true;
        }
    }

    public void CompleteLevel()
    {
        SaveManager.Instance.CompleteLevel(GameManager.Instance.currLevel);

        GameManager.Instance.menu = 1;

        ExitScene();
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
