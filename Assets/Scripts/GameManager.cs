using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public Material planeMaterial;
    public Color[] planeColors = new Color[10];
    public GameObject[] planeTrails = new GameObject[10];

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public int currLevel = 0;
    public int menu = 0;

    public Vector3 GetPlayerInput()
    {
        float stepSize = 0.1f;
        if (Input.GetKey(KeyCode.UpArrow))
            return Vector3.up * stepSize;
        else if (Input.GetKey(KeyCode.DownArrow))
            return Vector3.down * stepSize;
        else if (Input.GetKey(KeyCode.LeftArrow))
            return Vector3.left * stepSize;
        else if (Input.GetKey(KeyCode.RightArrow))
            return Vector3.right * stepSize;
        else
            return Vector3.zero;
    }
}
