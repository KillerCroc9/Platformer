using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject WinPanel;
    private void Start()
    {
        WinPanel = GameObject.FindGameObjectWithTag("winPanel"); //Gets win UI
        WinPanel = WinPanel.transform.GetChild(1).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // If player enters trigger
        {
            Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
    }
}
