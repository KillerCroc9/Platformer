using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{

    public GameObject Puase;
    public GameObject Panel;
    public GameObject Level;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Puase.SetActive(true);
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        Puase.SetActive(false);
    }
    public void Menu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Next()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Close()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Play() 
    {  
       Level.SetActive(true);
    }
}
