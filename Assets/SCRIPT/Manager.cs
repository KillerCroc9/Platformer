using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public GameObject Puase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            Time.timeScale = 0;
            Puase.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
