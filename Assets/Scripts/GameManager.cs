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
}
