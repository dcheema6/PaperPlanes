using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveGame game;

    void Awake()
    {
        // ResetSave(); // For dev purposes
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

    public bool IsColorOwned(int ind)
    {
        return (game.color & (1 << ind)) != 0;
    }

    public bool IsTrailOwned(int ind)
    {
        return (game.trail & (1 << ind)) != 0;
    }

    public bool BuyColor(int ind, int cost)
    {
        if (game.gold >= cost)
        {
            game.gold -= cost;
            UnlockColor(ind);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BuyTrail(int ind, int cost)
    {
        if (game.gold >= cost)
        {
            game.gold -= cost;
            UnlockTrail(ind);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UnlockColor(int ind)
    {
        game.color |= 1 << ind;
    }

    public void UnlockTrail(int ind)
    {
        game.trail |= 1 << ind;
    }

    public void CompleteLevel(int ind)
    {
        if (game.levelCompleted == ind)
        {
            game.levelCompleted++;
            Save();
        }
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }
}
