using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveGame game;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveGame>(game));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            game = Helper.Deserialize<SaveGame>(PlayerPrefs.GetString("save"));
        }
        else
        {
            game = new SaveGame();
            Save();
            Debug.Log("No save game found! Saving...");
        }
    }
}
