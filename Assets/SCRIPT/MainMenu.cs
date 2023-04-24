using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(int levelNo)
    {
        var levelData = LevelDataManager.GetLevelData(levelNo); //GetLevelData is a static method in LevelDataManager
        
        if (levelData != null)
        {
            PlayerPrefs.SetInt("mazeSize", levelData.mazeSize);
            PlayerPrefs.SetInt("trapProb", levelData.trapProb);
            PlayerPrefs.SetInt("levelNo", levelNo);
            SceneManager.LoadScene(levelData.sceneName);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

}
