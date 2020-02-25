using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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
