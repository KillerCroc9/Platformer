using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{

    public GameObject Puase;

    private void Update()
    {
        print(PlayerPrefs.GetInt("levelNo"));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Puase.SetActive(true);
        }
    }
    public void Resume() //resume button
    {
        Time.timeScale = 1;
        Puase.SetActive(false);
    }
    public void Menu() //menu button
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Close()
    {
        Puase.SetActive(false);
        Time.timeScale = 1;
    }
    public void NextLevel() //next level button
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("levelNo") >= 12)
        {
            Menu();
        }
        PlayerPrefs.SetInt("levelNo", PlayerPrefs.GetInt("levelNo") + 1);
        var levelData = LevelDataManager.GetLevelData(PlayerPrefs.GetInt("levelNo")); //Get level data from LevelDataManager
        if (levelData != null)
        {
                PlayerPrefs.SetInt("mazeSize", levelData.mazeSize);
                PlayerPrefs.SetInt("trapProb", levelData.trapProb);
                SceneManager.LoadScene(levelData.sceneName);
        }
    }
   

}
