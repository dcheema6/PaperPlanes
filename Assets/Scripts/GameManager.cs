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
        Vector3 r = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            r += Vector3.back;
        if (Input.GetKey(KeyCode.DownArrow))
            r +=  Vector3.forward;
        if (Input.GetKey(KeyCode.LeftArrow))
            r +=  Vector3.down;
        if (Input.GetKey(KeyCode.RightArrow))
            r +=  Vector3.up;
        return r;
    }
}
