using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int levelNo;
    public int mazeSize;
    public int trapProb;
    public string sceneName;
}

public static class LevelDataManager
{
    private static List<LevelData> levelDataList = new List<LevelData>() //holds all the level data
    {
        new LevelData() { levelNo = 1, mazeSize = 10, trapProb = 15, sceneName = "Meadow Mayhem" },
        new LevelData() { levelNo = 2, mazeSize = 15, trapProb = 15, sceneName = "Meadow Mayhem" },
        new LevelData() { levelNo = 3, mazeSize = 20, trapProb = 20, sceneName = "Meadow Mayhem" },
        new LevelData() { levelNo = 4, mazeSize = 20, trapProb = 20, sceneName = "Meadow Mayhem Night" },
        new LevelData() { levelNo = 5, mazeSize = 25, trapProb = 25, sceneName = "Meadow Mayhem Night" },
        new LevelData() { levelNo = 6, mazeSize = 25, trapProb = 25, sceneName = "Meadow Mayhem Night" },
        new LevelData() { levelNo = 7, mazeSize = 30, trapProb = 30, sceneName = "Retro Doom" },
        new LevelData() { levelNo = 8, mazeSize = 30, trapProb = 30, sceneName = "Retro Doom" },
        new LevelData() { levelNo = 9, mazeSize = 35, trapProb = 35, sceneName = "Retro Doom" },
        new LevelData() { levelNo = 10, mazeSize = 35, trapProb = 35, sceneName = "Retro Doom Night" },
        new LevelData() { levelNo = 11, mazeSize = 40, trapProb = 40, sceneName = "Retro Doom Night" },
        new LevelData() { levelNo = 12, mazeSize = 40, trapProb = 50, sceneName = "Retro Doom Night" },
        // add more levels here
    };

    public static LevelData GetLevelData(int levelNo) //returns the level data for the given level number
    {
        foreach (var levelData in levelDataList)
        {
            if (levelData.levelNo == levelNo)
            {
                return levelData;
            }
        }
        return null;
    }
}
